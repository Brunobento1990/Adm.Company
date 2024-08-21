using Adm.Company.Api.Attributes;
using Adm.Company.Application.Interfaces.Atendimento;
using Adm.Company.Application.ViewModel.Atendimentos;
using Adm.Company.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Adm.Company.Api.Controllers.Atendimentos;

[ApiController]
[Route("mensagem-atendimento")]
[AutenticaUsuario]
[AutenticaEmpresa]
public class MensagemAtendimentoController : ControllerBase
{
    private readonly IMensagemAtendimentoService _mensagemAtendimentoService;

    public MensagemAtendimentoController(IMensagemAtendimentoService mensagemAtendimentoService)
    {
        _mensagemAtendimentoService = mensagemAtendimentoService;
    }

    [HttpGet]
    [ProducesResponseType<IList<MensagemAtendimentoViewModel>>(200)]
    [ProducesResponseType<ErrorResponse>(401)]
    [ProducesResponseType<ErrorResponse>(400)]
    public async Task<IActionResult> GetMensagensDoAtendimento([FromQuery] Guid atendimentoId)
    {
        var mensagens = await _mensagemAtendimentoService.MensagensDoAtendimentoAsync(atendimentoId);
        return Ok(mensagens);
    }
}
