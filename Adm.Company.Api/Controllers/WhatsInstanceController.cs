using Adm.Company.Api.Attributes;
using Adm.Company.Application.Interfaces;
using Adm.Company.Application.ViewModel;
using Adm.Company.Application.ViewModel.WhatsApi;
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
    [ProducesResponseType<ConnectInstanceViewModel>(200)]
    [ProducesResponseType<ErrorResponse>(401)]
    [ProducesResponseType<ErrorResponse>(400)]
    public async Task<IActionResult> Instance()
    {
        var response = await _whatsServiceInstanceService.ConnectInstanceAsync();
        return Ok(response);
    }
}
