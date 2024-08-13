
using Adm.Company.Domain.Enums;
using Microsoft.AspNetCore.SignalR;

namespace Adm.Company.Application.Hubs;

public class QrCodeUpdateWhatsHub : Hub
{
    public async Task UpdateQrCode(string qrCode)
    {
        await Clients.All.SendAsync(nameof(EnumHub.UpdateQrCode), qrCode);
    }

    public async Task UpdateConnetion(string status)
    {
        await Clients.All.SendAsync(nameof(EnumHub.Conexao), status);
    }
}