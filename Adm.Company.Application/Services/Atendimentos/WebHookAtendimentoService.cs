using Adm.Company.Application.Hubs;
using Adm.Company.Application.Interfaces.Atendimento;
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

    public WebHookAtendimentoService(
        IConfiguracaoAtendimentoEmpresaRepository configuracaoAtendimentoEmpresaRepository,
        IAtendimentoRepository atendimentoRepository,
        IHubContext<WhatsHub> hubContext)
    {
        _configuracaoAtendimentoEmpresaRepository = configuracaoAtendimentoEmpresaRepository;
        _atendimentoRepository = atendimentoRepository;
        _hubContext = hubContext;
    }

    public async Task CreateOrUpdateAtendimentoWebHookAsync(
        string mensagem,
        string numeroWhatsEmpresa,
        string numeroWhatsOrigem)
    {
        var configuracaoAtendimento = await _configuracaoAtendimentoEmpresaRepository
            .GetConfiguracaoAtendimentoEmpresaByNumeroWhtasAsync(numeroWhatsEmpresa);

        if (configuracaoAtendimento == null) return;

        var atendimento = await _atendimentoRepository
            .GetAtendimentoByStatusAsync(StatusAtendimento.Aberto, numeroWhatsOrigem, configuracaoAtendimento.EmpresaId);

        if (atendimento == null)
        {
            atendimento = Atendimento.Factorie.Iniciar(numeroWhatsOrigem, mensagem, configuracaoAtendimento.EmpresaId);
            await _atendimentoRepository.AddAsync(atendimento);
            await _hubContext.Clients.All.SendAsync(nameof(EnumHub.NovoAtendimento), new 
            {
                whatsApp = configuracaoAtendimento.WhatsApp,
            });
            return;
        }
    }
}
