using Adm.Company.Api.Attributes;
using Adm.Company.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Adm.Company.Api.Controllers;

[ApiController]
[Route("instance")]
[AutenticaUsuario]
[AutenticaEmpresa]
public class WhatsInstanceController : ControllerBase
{
    private readonly IWhatsServiceInstanceService _whatsServiceInstanceService;

    public WhatsInstanceController(IWhatsServiceInstanceService whatsServiceInstanceService)
    {
        _whatsServiceInstanceService = whatsServiceInstanceService;
    }

    [HttpGet]
    public async Task<IActionResult> Instance()
    {
        var response = await _whatsServiceInstanceService.ConnectInstanceAsync();
        return Ok(response);
    }

    [HttpGet("perfil")]
    public async Task<IActionResult> Perfil()
    {
        var response = await _whatsServiceInstanceService.GetPerfilAsync();
        return Ok(response);
    }
}
