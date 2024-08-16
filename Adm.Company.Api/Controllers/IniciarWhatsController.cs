using Adm.Company.Api.Attributes;
using Adm.Company.Application.ViewModel.WhatsApi;
using Adm.Company.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Adm.Company.Application.Interfaces;

namespace Adm.Company.Api.Controllers;

[ApiController]
[Route("iniciar")]
[AutenticaUsuario]
[AutenticaEmpresa]
public class IniciarWhatsController : ControllerBase
{
    private readonly IIniciarWhatsService _iniciarWhtasService;

    public IniciarWhatsController(IIniciarWhatsService iniciarWhtasService)
    {
        _iniciarWhtasService = iniciarWhtasService;
    }

    [HttpGet]
    [ProducesResponseType<IniciarWhatsViewModel>(200)]
    [ProducesResponseType<ErrorResponse>(401)]
    [ProducesResponseType<ErrorResponse>(400)]
    public async Task<IActionResult> Perfil()
    {
        var response = await _iniciarWhtasService.GetPerfilAsync();
        return Ok(response);
    }
}
