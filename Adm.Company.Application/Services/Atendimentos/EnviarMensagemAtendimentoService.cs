using Adm.Company.Application.Dtos.Atendimentos;
using Adm.Company.Application.Helpers;
using Adm.Company.Application.Interfaces.Atendimento;
using Adm.Company.Application.ViewModel.Atendimentos;
using Adm.Company.Domain.Enums;
using Adm.Company.Domain.Exceptions;
using Adm.Company.Domain.Interfaces;
using Adm.Company.Infrastructure.HttpServices.Interfaces;
using Adm.Company.Infrastructure.HttpServices.Requests.WhtasApi;
using static Adm.Company.Domain.Entities.MensagemAtendimento;

namespace Adm.Company.Application.Services.Atendimentos;

public sealed class EnviarMensagemAtendimentoService : IEnviarMensagemAtendimentoService
{
    private readonly IAtendimentoRepository _atendimentoRepository;
    private readonly IConfiguracaoAtendimentoEmpresaRepository _configuracaoAtendimentoEmpresaRepository;
    private readonly IUsuarioAutenticado _usuarioAutenticado;
    private readonly IMensagemAtendimentoRepository _mensagemAtendimentoRepository;
    private readonly IChatWhatsHttpService _chatWhatsHttpService;

    public EnviarMensagemAtendimentoService(
        IAtendimentoRepository atendimentoRepository,
        IConfiguracaoAtendimentoEmpresaRepository configuracaoAtendimentoEmpresaRepository,
        IUsuarioAutenticado usuarioAutenticado,
        IMensagemAtendimentoRepository mensagemAtendimentoRepository,
        IChatWhatsHttpService chatWhatsHttpService)
    {
        _atendimentoRepository = atendimentoRepository;
        _configuracaoAtendimentoEmpresaRepository = configuracaoAtendimentoEmpresaRepository;
        _usuarioAutenticado = usuarioAutenticado;
        _mensagemAtendimentoRepository = mensagemAtendimentoRepository;
        _chatWhatsHttpService = chatWhatsHttpService;
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

        var result = await EnviarMensagemAsync(
            instanceName: configuracaoAtendimento.WhatsApp,
            remoteJid: atendimento.Cliente.RemoteJid ?? string.Empty,
            mensagem: enviarMensagemAtendimentoDto.Mensagem,
            audio: enviarMensagemAtendimentoDto.Audio,
            imagem: enviarMensagemAtendimentoDto.Imagem);

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
            descricaoFoto: enviarMensagemAtendimentoDto.Mensagem);

        await _mensagemAtendimentoRepository.AddAsync(mensagemAtendimento);

        return (MensagemAtendimentoViewModel)mensagemAtendimento;
    }

    private async Task<string?> EnviarMensagemAsync(
        string instanceName,
        string remoteJid,
        string mensagem,
        string? audio,
        string? imagem)
    {
        if (!string.IsNullOrWhiteSpace(audio))
        {
            var response = await _chatWhatsHttpService.EnviarAudioAsync(instanceName, new EnviarAudioRequest()
            {
                Audio = audio,
                Number = remoteJid
            });

            return response?.Key?.Id;
        }

        if (!string.IsNullOrWhiteSpace(imagem))
        {
            var response = await _chatWhatsHttpService.EnviaImagemAsync(instanceName, new EnviarImagemRequest()
            {
                Number = remoteJid,
                Caption = mensagem,
                Media = imagem,
            });

            return response?.Key?.Id;
        }

        var result = await _chatWhatsHttpService.EnviarMensagemAsync(instanceName, new EnviarMensagemRequest()
        {
            Number = remoteJid,
            Text = mensagem
        });

        return result?.Key?.Id;
    }
}
