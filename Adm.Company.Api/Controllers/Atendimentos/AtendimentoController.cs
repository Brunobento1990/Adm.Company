using Adm.Company.Api.Attributes;
using Adm.Company.Application.Interfaces.Atendimento;
using Adm.Company.Application.ViewModel;
using Adm.Company.Application.ViewModel.Atendimentos;
using Microsoft.AspNetCore.Mvc;

namespace Adm.Company.Api.Controllers.Atendimentos;

[ApiController]
[Route("atendimento")]
[AutenticaUsuario]
[AutenticaEmpresa]
public class AtendimentoController : ControllerBase
{
    private readonly IAtendimentoService _atendimentoService;

    public AtendimentoController(IAtendimentoService atendimentoService)
    {
        _atendimentoService = atendimentoService;
    }

    [HttpGet("meus-atendimentos")]
    [ProducesResponseType<IList<AtendimentoViewModel>>(200)]
    [ProducesResponseType<ErrorResponse>(401)]
    [ProducesResponseType<ErrorResponse>(400)]
    public async Task<IActionResult> MeusAtendimentos()
    {
        var atendimentosViewModel = await _atendimentoService.MeusAtendimentosAsync();
        return Ok(atendimentosViewModel);
    }

    [HttpGet("atendimentos-aberto")]
    [ProducesResponseType<IList<AtendimentoViewModel>>(200)]
    [ProducesResponseType<ErrorResponse>(401)]
    [ProducesResponseType<ErrorResponse>(400)]
    public async Task<IActionResult> AtendimentosAberto()
    {
        var atendimentosViewModel = await _atendimentoService.AtendimentosEmAbertoAsync();
        return Ok(atendimentosViewModel);
    }

    [HttpPut("iniciar-atendimento")]
    [ProducesResponseType<AtendimentoViewModel>(200)]
    [ProducesResponseType<ErrorResponse>(401)]
    [ProducesResponseType<ErrorResponse>(400)]
    public async Task<IActionResult> AtendimentosAberto([FromQuery] Guid atendimentoId)
    {
        var atendimentosViewModel = await _atendimentoService.IniciarAtendimentoAsync(atendimentoId);
        return Ok(atendimentosViewModel);
    }
}
