using Adm.Company.Application.Dtos.Login;
using Adm.Company.Application.Interfaces;
using Adm.Company.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Adm.Company.Api.Controllers;

[ApiController]
[Route("login")]
public class LoginController : ControllerBase
{
    private readonly ILoginService _loginService;

    public LoginController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpPost("usuario")]
    [ProducesResponseType<LoginViewModel>(200)]
    [ProducesResponseType<ErrorResponse>(401)]
    [ProducesResponseType<ErrorResponse>(400)]
    public async Task<IActionResult> LoginUsuario(LoginUsuarioDto loginUsuarioDto)
    {
        var usuarioResponse = await _loginService.LoginAsync(loginUsuarioDto);
        return Ok(usuarioResponse);
    }
}
