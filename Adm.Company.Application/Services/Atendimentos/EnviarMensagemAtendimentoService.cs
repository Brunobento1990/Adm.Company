using Adm.Company.Application.Dtos.Atendimentos;
using Adm.Company.Application.Helpers;
using Adm.Company.Application.Interfaces.Atendimentos;
using Adm.Company.Application.ViewModel.Atendimentos;
using Adm.Company.Domain.Entities;
using Adm.Company.Domain.Enums;
using Adm.Company.Domain.Exceptions;
using Adm.Company.Domain.Interfaces;
using Adm.Company.Infrastructure.HttpServices.Interfaces;
using Adm.Company.Infrastructure.HttpServices.Requests.WhtasApi;
using Adm.Company.Infrastructure.HttpServices.Responses.WhatsApi;
using static Adm.Company.Domain.Entities.MensagemAtendimento;

namespace Adm.Company.Application.Services.Atendimentos;

public sealed class EnviarMensagemAtendimentoService : IEnviarMensagemAtendimentoService
{
    private readonly IAtendimentoRepository _atendimentoRepository;
    private readonly IConfiguracaoAtendimentoEmpresaRepository _configuracaoAtendimentoEmpresaRepository;
    private readonly IUsuarioAutenticado _usuarioAutenticado;
    private readonly IMensagemAtendimentoRepository _mensagemAtendimentoRepository;
    private readonly IChatWhatsHttpService _chatWhatsHttpService;
    private readonly IWhatsHttpService _whatsHttpService;

    public EnviarMensagemAtendimentoService(
        IAtendimentoRepository atendimentoRepository,
        IConfiguracaoAtendimentoEmpresaRepository configuracaoAtendimentoEmpresaRepository,
        IUsuarioAutenticado usuarioAutenticado,
        IMensagemAtendimentoRepository mensagemAtendimentoRepository,
        IChatWhatsHttpService chatWhatsHttpService,
        IWhatsHttpService whatsHttpService)
    {
        _atendimentoRepository = atendimentoRepository;
        _configuracaoAtendimentoEmpresaRepository = configuracaoAtendimentoEmpresaRepository;
        _usuarioAutenticado = usuarioAutenticado;
        _mensagemAtendimentoRepository = mensagemAtendimentoRepository;
        _chatWhatsHttpService = chatWhatsHttpService;
        _whatsHttpService = whatsHttpService;
    }

    public async Task<MensagemAtendimentoViewModel> EnviarMensagemAsync(
        EnviarMensagemAtendimentoDto enviarMensagemAtendimentoDto)
    {
        var atendimento = await _atendimentoRepository
                .GetByIdAsync(enviarMensagemAtendimentoDto.AtendimentoId)
            ?? throw new ExceptionApiErro("Não foi possível localizar o atendimento!");

        var configuracaoAtendimento = await _configuracaoAtendimentoEmpresaRepository
                .GetConfiguracaoAtendimentoEmpresaByEmpresaIdAsync(_usuarioAutenticado.EmpresaId)
            ?? throw new ExceptionApiErro("Não foi possível localizar as configurações de atendimento!");

        var result = await EnviarMensagemLocalAsync(
            instanceName: configuracaoAtendimento.WhatsApp,
            remoteJid: atendimento.Cliente.RemoteJid ?? string.Empty,
            mensagem: enviarMensagemAtendimentoDto.Mensagem,
            audio: enviarMensagemAtendimentoDto.Audio,
            imagem: enviarMensagemAtendimentoDto.Imagem,
            resposta: enviarMensagemAtendimentoDto.Resposta,
            respostaId: enviarMensagemAtendimentoDto.RespostaId);

        if (string.IsNullOrWhiteSpace(result))
        {
            throw new ExceptionApiErro("Não foi possível enviar a mensagem!");
        }

        var audio = enviarMensagemAtendimentoDto.Audio?.ConverterStringParaBytes();
        var imagem = enviarMensagemAtendimentoDto.Imagem?.ConverterStringParaBytes();
        var figurinha = enviarMensagemAtendimentoDto.Figurinha?.ConverterStringParaBytes();

        var mensagemAtendimento = FabricaMensagem.Fabricar(
            mensagem: enviarMensagemAtendimentoDto.Mensagem,
            minhaMensagem: true,
            remoteId: result,
            atendimentoId: atendimento.Id,
            audio: audio,
            status: StatusMensagem.Enviado,
            imagem: imagem,
            figurinha: figurinha,
            descricaoFoto: imagem != null ? enviarMensagemAtendimentoDto.Mensagem : null,
            resposta: enviarMensagemAtendimentoDto.Resposta,
            respostaId: enviarMensagemAtendimentoDto.RespostaId?.ToString());

        await _mensagemAtendimentoRepository.AddAsync(mensagemAtendimento);

        return (MensagemAtendimentoViewModel)mensagemAtendimento;
    }

    public async Task<MensagemAtendimentoViewModel> EnviarPrimeiraMensagemMensagemAsync(
        string remoteJid,
        ConfiguracaoAtendimentoEmpresa configuracaoAtendimento,
        Atendimento atendimento)
    {
        if (!string.IsNullOrWhiteSpace(configuracaoAtendimento.PrimeiraMensagem))
        {
            var result = await EnviarMensagemLocalAsync(
            instanceName: configuracaoAtendimento.WhatsApp,
            remoteJid: remoteJid,
            mensagem: configuracaoAtendimento.PrimeiraMensagem,
            audio: null,
            imagem: null,
            resposta: null,
            respostaId: null);


            if (!string.IsNullOrWhiteSpace(result))
            {
                var mensagemAtendimento = FabricaMensagem.Fabricar(
                mensagem: configuracaoAtendimento.PrimeiraMensagem,
                minhaMensagem: true,
                remoteId: result,
                atendimentoId: atendimento.Id,
                audio: null,
                status: StatusMensagem.Enviado,
                imagem: null,
                figurinha: null,
                descricaoFoto: null,
                resposta: null,
                respostaId: null);

                await _mensagemAtendimentoRepository.AddAsync(mensagemAtendimento);

                return (MensagemAtendimentoViewModel)mensagemAtendimento;
            }
        }

        return new();
    }

    private async Task<string?> EnviarMensagemLocalAsync(
        string instanceName,
        string remoteJid,
        string mensagem,
        string? audio,
        string? imagem,
        string? resposta,
        Guid? respostaId)
    {

        Quoted? quoted = null;

        if (respostaId.HasValue && !string.IsNullOrWhiteSpace(resposta))
        {
            var mensagemAtendimento = await _mensagemAtendimentoRepository.GetByIdAsync(respostaId.Value);

            if (mensagemAtendimento != null)
            {
                quoted = new Quoted()
                {
                    Key = new QuotedKey()
                    {
                        FromMe = true,
                        RemoteJid = remoteJid,
                        Id = mensagemAtendimento.RemoteId
                    },
                    QuotedMessage = new()
                    {
                        Conversation = resposta
                    }
                };
            }
        }

        if (!string.IsNullOrWhiteSpace(audio))
        {
            var (response, erroAudio) = await _chatWhatsHttpService.EnviarAudioAsync(instanceName, new EnviarAudioRequest()
            {
                Audio = audio,
                Number = remoteJid,
                Quoted = quoted
            });

            await TratarErroEnvioMensagem(erroAudio, instanceName);

            return response?.Key?.Id;
        }

        if (!string.IsNullOrWhiteSpace(imagem))
        {
            var (response, erroImagem) = await _chatWhatsHttpService.EnviaImagemAsync(instanceName, new EnviarImagemRequest()
            {
                Number = remoteJid,
                Caption = mensagem,
                Media = imagem,
                Quoted = quoted
            });

            await TratarErroEnvioMensagem(erroImagem, instanceName);

            return response?.Key?.Id;
        }

        var (result, erro) = await _chatWhatsHttpService.EnviarMensagemAsync(instanceName, new EnviarMensagemRequest()
        {
            Number = remoteJid,
            Text = mensagem,
            Quoted = quoted
        });

        await TratarErroEnvioMensagem(erro, instanceName);

        return result?.Key?.Id;
    }

    private async Task TratarErroEnvioMensagem(ErroEnvioMensagemResponse? erro, string instanceName)
    {
        if (erro != null)
        {
            if (erro.Status == 500 && erro.Response?.Message == "Connection Closed")
            {
                await _whatsHttpService.LogoutInstanceAsync(instanceName);
                await _whatsHttpService.DeleteInstanceAsync(instanceName);
                throw new ExceptionApiErro("É necessário conectar ao whats app novamente!");
            }
        }
    }
}
