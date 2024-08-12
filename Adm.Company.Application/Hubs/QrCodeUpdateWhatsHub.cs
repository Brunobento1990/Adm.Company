
using Microsoft.AspNetCore.SignalR;

namespace Adm.Company.Application.Hubs;

public class QrCodeUpdateWhatsHub : Hub
{
    public async Task UpdateQrCode(string qrCode)
    {
        await Clients.All.SendAsync("UpdateQrCode", qrCode);
    }

    public async Task UpdateConnetion(string qrCode)
    {
        await Clients.All.SendAsync("Connecting", qrCode);
    }
}