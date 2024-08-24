using Adm.Company.Application.Helpers;
using Adm.Company.Application.Hubs;
using Adm.Company.Application.Interfaces.Atendimento;
using Adm.Company.Application.ViewModel.Atendimentos;
using Adm.Company.Domain.Entities;
using Adm.Company.Domain.Enums;
using Adm.Company.Domain.Exceptions;
using Adm.Company.Domain.Interfaces;
using Adm.Company.Infrastructure.HttpServices.Interfaces;
using Adm.Company.Infrastructure.HttpServices.Requests.WhtasApi;
using Adm.Company.Infrastructure.Migrations;
using Microsoft.AspNetCore.SignalR;
using System.Text;

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
        bool fromMe)
    {
        var configuracaoAtendimento = await _configuracaoAtendimentoEmpresaRepository
            .GetConfiguracaoAtendimentoEmpresaByNumeroWhtasAsync(numeroWhatsEmpresa);

        if (configuracaoAtendimento == null) return;

        byte[]? audio = await MensagemAudioAsync(
            tipoMensagem: tipoMensagem,
            instanceName: configuracaoAtendimento.WhatsApp,
            remoteId: remoteId);

        var atendimento = await _atendimentoRepository
            .GetAtendimentoByStatusAsync(StatusAtendimento.EmAndamento, numeroWhatsOrigem, configuracaoAtendimento.EmpresaId);

        if (atendimento == null)
        {
            var numeroWhatsTratado = ConvertWhatsHelpers.ConvertRemoteJidWhats(numeroWhatsOrigem);
            var cliente = await _clienteRepository.GetByNumeroWhatsAsync(numeroWhatsTratado);

            if (cliente == null)
            {
                var perfil = await _chatWhatsHttpService.GetPerfilAsync(configuracaoAtendimento.WhatsApp);

                cliente = new Cliente(
                    id: Guid.NewGuid(),
                    criadoEm: DateTime.Now,
                    atualizadoEm: DateTime.Now,
                    numero: 0,
                    empresaId: configuracaoAtendimento.EmpresaId,
                    cpf: "sem CPF",
                    whatsApp: numeroWhatsTratado,
                    email: "sem email",
                    foto: perfil?.FirstOrDefault()?.ProfilePicUrl,
                    nome: nome.Length > 255 ? nome[..255] : nome,
                    remoteJid: numeroWhatsOrigem);

                await _clienteRepository.AddAsync(cliente);
            }

            atendimento = Atendimento.Factorie.Iniciar(
                mensagem,
                configuracaoAtendimento.EmpresaId,
                remoteId,
                tipoMensagem,
                fromMe,
                cliente.Id,
                audio: audio);

            await _atendimentoRepository.AddAsync(atendimento);
            await _hubContext.Clients.All.SendAsync(nameof(EnumHub.NovoAtendimento), new
            {
                whatsApp = configuracaoAtendimento.WhatsApp,
            });
            return;
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
            audio: audio);

        await _mensagemAtendimentoRepository.AddAsync(novaMensagem);

        await _hubContext.Clients.All.SendAsync(nameof(EnumHub.NovaMensagem), new
        {
            whatsApp = configuracaoAtendimento.WhatsApp,
            mensagemAtendimento = (MensagemAtendimentoViewModel)novaMensagem
        });
    }

    private async Task<byte[]?> MensagemAudioAsync(string tipoMensagem, string instanceName, string remoteId)
    {
        if (tipoMensagem == "audioMessage")
        {
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

        return null;
    }
}
