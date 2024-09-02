using Adm.Company.Api.Attributes;
using Adm.Company.Application.Interfaces;
using Adm.Company.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Adm.Company.Api.Controllers;

[ApiController]
[Route("usuario")]
[AutenticaUsuario]
[AutenticaEmpresa]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpGet("paginacao-drop-down")]
    [ProducesResponseType<IList<UsuarioViewModel>>(200)]
    [ProducesResponseType<ErrorResponse>(401)]
    [ProducesResponseType<ErrorResponse>(400)]
    public async Task<IActionResult> PaginacaoDropDown([FromQuery] int skip, [FromQuery] string? search)
    {
        var usuarios = await _usuarioService.GetPaginacaoAsync(skip, search);
        return Ok(usuarios);
    }
}
