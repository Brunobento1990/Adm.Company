using Adm.Company.Application.Helpers;
using Adm.Company.Application.Hubs;
using Adm.Company.Application.Interfaces.Atendimento;
using Adm.Company.Application.ViewModel.Atendimentos;
using Adm.Company.Domain.Entities;
using Adm.Company.Domain.Enums;
using Adm.Company.Domain.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Adm.Company.Application.Services.Atendimentos;

public sealed class WebHookAtendimentoService : IWebHookAtendimentoService
{
    private readonly IConfiguracaoAtendimentoEmpresaRepository _configuracaoAtendimentoEmpresaRepository;
    private readonly IAtendimentoRepository _atendimentoRepository;
    private readonly IHubContext<WhatsHub> _hubContext;
    private readonly IClienteRepository _clienteRepository;
    private readonly IMensagemAtendimentoRepository _mensagemAtendimentoRepository;

    public WebHookAtendimentoService(
        IConfiguracaoAtendimentoEmpresaRepository configuracaoAtendimentoEmpresaRepository,
        IAtendimentoRepository atendimentoRepository,
        IHubContext<WhatsHub> hubContext,
        IClienteRepository clienteRepository,
        IMensagemAtendimentoRepository mensagemAtendimentoRepository)
    {
        _configuracaoAtendimentoEmpresaRepository = configuracaoAtendimentoEmpresaRepository;
        _atendimentoRepository = atendimentoRepository;
        _hubContext = hubContext;
        _clienteRepository = clienteRepository;
        _mensagemAtendimentoRepository = mensagemAtendimentoRepository;
    }

    public async Task CreateOrUpdateAtendimentoWebHookAsync(
        string mensagem,
        string numeroWhatsEmpresa,
        string numeroWhatsOrigem,
        string remoteId,
        string tipoMensagem,
        string nome)
    {
        var configuracaoAtendimento = await _configuracaoAtendimentoEmpresaRepository
            .GetConfiguracaoAtendimentoEmpresaByNumeroWhtasAsync(numeroWhatsEmpresa);

        if (configuracaoAtendimento == null) return;

        var atendimento = await _atendimentoRepository
            .GetAtendimentoByStatusAsync(StatusAtendimento.Aberto, numeroWhatsOrigem, configuracaoAtendimento.EmpresaId);

        if (atendimento == null)
        {
            var numeroWhatsTratado = ConvertWhatsHelpers.ConvertRemoteJidWhats(numeroWhatsOrigem);
            var cliente = await _clienteRepository.GetByNumeroWhatsAsync(numeroWhatsTratado);

            if (cliente == null)
            {
                cliente = new Cliente(
                    id: Guid.NewGuid(),
                    criadoEm: DateTime.Now,
                    atualizadoEm: null,
                    numero: 0,
                    empresaId: configuracaoAtendimento.EmpresaId,
                    cpf: "sem CPF",
                    whatsApp: numeroWhatsTratado,
                    email: "sem email",
                    foto: null,
                    nome: nome.Length > 255 ? nome[..255] : nome,
                    remoteJid: numeroWhatsOrigem);

                await _clienteRepository.AddAsync(cliente);
            }

            atendimento = Atendimento.Factorie.Iniciar(
                mensagem,
                configuracaoAtendimento.EmpresaId,
                remoteId,
                tipoMensagem,
                false,
                cliente.Id);

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
            atualizadoEm: null,
            numero: 0,
            mensagem: mensagem,
            status: StatusMensagem.Entregue,
            atendimentoId: atendimento.Id,
            tipoMensagem: tipoMensagem,
            remoteId: remoteId,
            minhaMensagem: false);

        await _mensagemAtendimentoRepository.AddAsync(novaMensagem);

        await _hubContext.Clients.All.SendAsync(nameof(EnumHub.NovaMensagem), new
        {
            whatsApp = configuracaoAtendimento.WhatsApp,
            mensagemAtendimento = (MensagemAtendimentoViewModel)novaMensagem
        });
    }
}
