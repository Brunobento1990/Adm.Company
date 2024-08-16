using Adm.Company.Application.Hubs;
using Adm.Company.Application.Interfaces.Atendimento;
using Adm.Company.Domain.Enums;
using Adm.Company.Infrastructure.HttpServices.Requests.WhtasApi;
using Adm.Company.Infrastructure.HttpServices.Responses.WhatsApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Adm.Company.Api.Controllers;

[ApiController]
[Route("webhook")]
public class WebHookWhatsController : ControllerBase
{
    private readonly IHubContext<WhatsHub> _hubContext;
    private readonly IWebHookAtendimentoService _webHookAtendimentoService;

    public WebHookWhatsController(
        IHubContext<WhatsHub> hubContext,
        IWebHookAtendimentoService webHookAtendimentoService)
    {
        _hubContext = hubContext;
        _webHookAtendimentoService = webHookAtendimentoService;
    }

    [HttpPost("qrcode-updated")]
    public async Task<IActionResult> QrCodeupdate([FromBody] UpdateQrCodeWebHookRequest body)
    {
        await _hubContext.Clients.All.SendAsync(nameof(EnumHub.UpdateQrCode), new
        {
            qrCode = body.Data.Qrcode.Base64,
            whatsApp = body.Instance
        });
        return Ok();
    }

    [HttpPost("connection-update")]
    public async Task<IActionResult> ConnectionUpdate([FromBody] ConnectionUpdateRequest body)
    {
        if (body != null && body.Data != null)
        {
            await _hubContext.Clients.All.SendAsync(nameof(EnumHub.Conexao), new
            {
                status = body.Data.State,
                whatsApp = body.Instance
            });
        }
        return Ok();
    }

    [HttpPost("messages-upsert")]
    public async Task<IActionResult> ReceberMensagem([FromBody] MensagemRecebidaWhatsResponse body)
    {

        if (body != null && body.Data != null)
        {
            await _webHookAtendimentoService
                .CreateOrUpdateAtendimentoWebHookAsync(
                    body.Data.Message.Conversation, 
                    body.Instance, 
                    body.Data.Owner);
        }
        return Ok();
    }
}
