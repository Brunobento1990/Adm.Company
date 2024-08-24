﻿using Adm.Company.Application.Hubs;
using Adm.Company.Application.Interfaces.Atendimento;
using Adm.Company.Application.ViewModel.WhatsApi;
using Adm.Company.Domain.Enums;
using Adm.Company.Infrastructure.HttpServices.Requests.WhtasApi;
using Adm.Company.Infrastructure.HttpServices.Responses.WhatsApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace Adm.Company.Api.Controllers;

[ApiController]
[Route("webhook")]
public class WebHookWhatsController : ControllerBase
{
    private readonly IHubContext<WhatsHub> _hubContext;
    private readonly IWebHookAtendimentoService _webHookAtendimentoService;
    private readonly IAtualizarMensagemAtendimentoService _atualizarMensagemAtendimentoService;

    public WebHookWhatsController(
        IHubContext<WhatsHub> hubContext,
        IWebHookAtendimentoService webHookAtendimentoService,
        IAtualizarMensagemAtendimentoService atualizarMensagemAtendimentoService)
    {
        _hubContext = hubContext;
        _webHookAtendimentoService = webHookAtendimentoService;
        _atualizarMensagemAtendimentoService = atualizarMensagemAtendimentoService;
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

        if (body != null && body.Data != null && body.Data.Key != null)
        {
            await _webHookAtendimentoService
                .CreateOrUpdateAtendimentoWebHookAsync(
                    mensagem: body.Data.Message?.Conversation ?? string.Empty,
                    numeroWhatsEmpresa: body.Instance,
                    numeroWhatsOrigem: body.Data.Key.RemoteJid,
                    remoteId: body.Data.Key.Id,
                    tipoMensagem: body.Data.MessageType,
                    nome: body.Data.PushName,
                    fromMe: body.Data.Key.FromMe);
        }
        return Ok();
    }

    [HttpPost("messages-update")]
    public async Task<IActionResult> UpdateMensagem([FromBody] UpdateMensagemWhatsViewModel body)
    {
        if (!string.IsNullOrWhiteSpace(body.Instance) && body.Data != null && !string.IsNullOrWhiteSpace(body.Data.KeyId))
        {
            await _atualizarMensagemAtendimentoService.AtualizarAsync(body.Instance, body.Data.Status, body.Data.KeyId);
        }

        return Ok();
    }
}
