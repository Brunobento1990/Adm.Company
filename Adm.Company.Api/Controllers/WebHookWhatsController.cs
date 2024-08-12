using Adm.Company.Application.Hubs;
using Adm.Company.Domain.Interfaces;
using Adm.Company.Infrastructure.HttpServices.Requests.WhtasApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Adm.Company.Api.Controllers;

[ApiController]
[Route("webhook")]
public class WebHookWhatsController : ControllerBase
{
    private readonly IHubContext<QrCodeUpdateWhatsHub> _hubContext;

    public WebHookWhatsController(IHubContext<QrCodeUpdateWhatsHub> hubContext)
    {
        _hubContext = hubContext;
    }

    [HttpPost("qrcode-updated")]
    public async Task<IActionResult> QrCodeupdate([FromBody] UpdateQrCodeWebHookRequest body)
    {
        await _hubContext.Clients.All.SendAsync("UpdateQrCode", new {
            qrCode = body.Data.Qrcode,
            cpf = body.Instance
        });
        return Ok();
    }

    [HttpPost("connection-update")]
    public async Task<IActionResult> ConnectionUpdate([FromBody] ConnectionUpdateRequest body)
    {
        if(body != null && body.Data != null)
        {
            await _hubContext.Clients.All.SendAsync("Connecting", new
            {
                status = body.Data.StatusReason,
                cpf = body.Instance
            });
        }
        return Ok();
    }
}
