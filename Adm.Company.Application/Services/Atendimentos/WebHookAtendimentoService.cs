using Adm.Company.Application.Helpers;
using Adm.Company.Application.Hubs;
using Adm.Company.Application.Interfaces.Atendimento;
using Adm.Company.Application.ViewModel.Atendimentos;
using Adm.Company.Domain.Entities;
using Adm.Company.Domain.Enums;
using Adm.Company.Domain.Interfaces;
using Adm.Company.Infrastructure.HttpServices.Interfaces;
using Adm.Company.Infrastructure.HttpServices.Requests.WhtasApi;
using Microsoft.AspNetCore.SignalR;
using System.Text;
using static Adm.Company.Domain.Entities.Cliente;

namespace Adm.Company.Application.Services.Atendimentos;

public sealed class WebHookAtendimentoService : IWebHookAtendimentoService
{
    private readonly IConfiguracaoAtendimentoEmpresaRepository _configuracaoAtendimentoEmpresaRepository;
    private readonly IAtendimentoRepository _atendimentoRepository;
    private readonly IHubContext<WhatsHub> _hubContext;
    private readonly IClienteRepository _clienteRepository;
    private readonly IMensagemAtendimentoRepository _mensagemAtendimentoRepository;
    private readonly IChatWhatsHttpService _chatWhatsHttpService;

    public WebHookAtendimentoService(
        IConfiguracaoAtendimentoEmpresaRepository configuracaoAtendimentoEmpresaRepository,
        IAtendimentoRepository atendimentoRepository,
        IHubContext<WhatsHub> hubContext,
        IClienteRepository clienteRepository,
        IMensagemAtendimentoRepository mensagemAtendimentoRepository,
        IChatWhatsHttpService chatWhatsHttpService)
    {
        _configuracaoAtendimentoEmpresaRepository = configuracaoAtendimentoEmpresaRepository;
        _atendimentoRepository = atendimentoRepository;
        _hubContext = hubContext;
        _clienteRepository = clienteRepository;
        _mensagemAtendimentoRepository = mensagemAtendimentoRepository;
        _chatWhatsHttpService = chatWhatsHttpService;
    }

    public async Task CreateOrUpdateAtendimentoWebHookAsync(
        string mensagem,
        string numeroWhatsEmpresa,
        string numeroWhatsOrigem,
        string remoteId,
        string tipoMensagem,
        string nome,
        bool fromMe,
        string? caption)
    {
        var configuracaoAtendimento = await _configuracaoAtendimentoEmpresaRepository
            .GetConfiguracaoAtendimentoEmpresaByNumeroWhtasAsync(numeroWhatsEmpresa);

        if (configuracaoAtendimento == null) return;

        byte[]? audio = await MensagemAudioAsync(
            tipoMensagem: tipoMensagem,
            instanceName: configuracaoAtendimento.WhatsApp,
            remoteId: remoteId);

        byte[]? figurinha = await MensagemFigurinhaAsync(
            tipoMensagem: tipoMensagem,
            instanceName: configuracaoAtendimento.WhatsApp,
            remoteId: remoteId);

        byte[]? imagem = await MensagemImagemAsync(
            tipoMensagem: tipoMensagem,
            instanceName: configuracaoAtendimento.WhatsApp,
            remoteId: remoteId);

        var atendimento = await _atendimentoRepository
            .GetAtendimentoByStatusAsync(StatusAtendimento.EmAndamento, StatusAtendimento.Aberto, numeroWhatsOrigem, configuracaoAtendimento.EmpresaId);

        if (atendimento == null)
        {
            lock (this) 
            {
                var numeroWhatsTratado = ConvertWhatsHelpers.ConvertRemoteJidWhats(numeroWhatsOrigem);
                var cliente = _clienteRepository.GetByNumeroWhatsAsync(numeroWhatsTratado, configuracaoAtendimento.EmpresaId).Result;

                if (cliente == null)
                {
                    var perfil = _chatWhatsHttpService.GetPerfilAsync(configuracaoAtendimento.WhatsApp).Result;

                    cliente = FactorieCliente.FactorieWhats(
                        empresaId: configuracaoAtendimento.EmpresaId,
                        numeroWhats: numeroWhatsTratado,
                        foto: perfil?.FirstOrDefault()?.ProfilePicUrl,
                        nome: nome,
                        remoteJid: numeroWhatsOrigem);

                    _clienteRepository.AddAsync(cliente).Wait();
                }

                atendimento = Atendimento.Factorie.Iniciar(
                    mensagem,
                    configuracaoAtendimento.EmpresaId,
                    remoteId,
                    tipoMensagem,
                    fromMe,
                    cliente.Id,
                    audio: audio,
                    figurinha: figurinha,
                    imagem: imagem,
                    descricaoFoto: caption);

                _atendimentoRepository.AddAsync(atendimento).Wait();
                _hubContext.Clients.All.SendAsync(nameof(EnumHub.NovoAtendimento), new
                {
                    whatsApp = configuracaoAtendimento.WhatsApp,
                }).Wait();
                return;
            }
        }

        var novaMensagem = new MensagemAtendimento(
            id: Guid.NewGuid(),
            criadoEm: DateTime.Now,
            atualizadoEm: DateTime.Now,
            numero: 0,
            mensagem: mensagem,
            status: StatusMensagem.Entregue,
            atendimentoId: atendimento.Id,
            tipoMensagem: tipoMensagem,
            remoteId: remoteId,
            minhaMensagem: fromMe,
            audio: audio,
            figurinha: figurinha,
            imagem: imagem,
            descricaoFoto: caption);

        await _mensagemAtendimentoRepository.AddAsync(novaMensagem);

        await _hubContext.Clients.All.SendAsync(nameof(EnumHub.NovaMensagem), new
        {
            whatsApp = configuracaoAtendimento.WhatsApp,
            mensagemAtendimento = (MensagemAtendimentoViewModel)novaMensagem
        });
    }

    private async Task<byte[]?> MensagemAudioAsync(string tipoMensagem, string instanceName, string remoteId)
    {
        if (tipoMensagem != nameof(TipoMensagemEnum.audioMessage))
        {
            return null;
        }
        var convertAudio = await _chatWhatsHttpService
                .ConvertAudioMensagemAsync(instanceName, new ConvertAudioRequest()
                {
                    ConvertToMp4 = false,
                    Message = new MessageConvertAudioRequest()
                    {
                        Key = new KeyConvertAudioRequest()
                        {
                            Id = remoteId
                        }
                    }
                });

        return convertAudio?.Base64 != null ? Encoding.UTF8.GetBytes(convertAudio.Base64) : null;
    }

    private async Task<byte[]?> MensagemFigurinhaAsync(string tipoMensagem, string instanceName, string remoteId)
    {
        if (tipoMensagem != nameof(TipoMensagemEnum.stickerMessage))
        {
            return null;
        }
        var convertAudio = await _chatWhatsHttpService
                .ConvertAudioMensagemAsync(instanceName, new ConvertAudioRequest()
                {
                    ConvertToMp4 = false,
                    Message = new MessageConvertAudioRequest()
                    {
                        Key = new KeyConvertAudioRequest()
                        {
                            Id = remoteId
                        }
                    }
                });

        return convertAudio?.Base64 != null ? Encoding.UTF8.GetBytes(convertAudio.Base64) : null;
    }

    private async Task<byte[]?> MensagemImagemAsync(string tipoMensagem, string instanceName, string remoteId)
    {
        if (tipoMensagem != nameof(TipoMensagemEnum.imageMessage))
        {
            return null;
        }
        var convertAudio = await _chatWhatsHttpService
                .ConvertAudioMensagemAsync(instanceName, new ConvertAudioRequest()
                {
                    ConvertToMp4 = false,
                    Message = new MessageConvertAudioRequest()
                    {
                        Key = new KeyConvertAudioRequest()
                        {
                            Id = remoteId
                        }
                    }
                });

        return convertAudio?.Base64 != null ? Encoding.UTF8.GetBytes(convertAudio.Base64) : null;
    }
}
