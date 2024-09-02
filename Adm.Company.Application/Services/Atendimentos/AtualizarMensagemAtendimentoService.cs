using Adm.Company.Application.Helpers;
using Adm.Company.Application.Hubs;
using Adm.Company.Application.Interfaces.Atendimentos;
using Adm.Company.Domain.Enums;
using Adm.Company.Domain.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Adm.Company.Application.Services.Atendimentos;

public sealed class AtualizarMensagemAtendimentoService : IAtualizarMensagemAtendimentoService
{
    private readonly IMensagemAtendimentoRepository _mensagemAtendimentoRepository;
    private readonly IHubContext<WhatsHub> _hubContext;

    public AtualizarMensagemAtendimentoService(
        IMensagemAtendimentoRepository mensagemAtendimentoRepository,
        IHubContext<WhatsHub> hubContext)
    {
        _mensagemAtendimentoRepository = mensagemAtendimentoRepository;
        _hubContext = hubContext;
    }

    public async Task AtualizarAsync(string instance, string status, string remoteId)
    {
        var mensagem = await _mensagemAtendimentoRepository.GetByRemoteIdAsync(remoteId);
        if (mensagem == null) return;
        var novoStatus = ConvertWhatsHelpers.ConvertStatus(status);
        if (novoStatus == null) return;
        mensagem.UpdateStatus(novoStatus.Value);

        await _mensagemAtendimentoRepository.UpdateAsync(mensagem);

        await _hubContext.Clients.All.SendAsync(nameof(EnumHub.UpdateMensagem), new
        {
            whatsApp = instance,
            mensagemAtendimentoId = mensagem.Id,
            status = mensagem.Status
        });
    }
}
