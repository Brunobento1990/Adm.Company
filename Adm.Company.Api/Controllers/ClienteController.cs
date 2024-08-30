using Adm.Company.Api.Attributes;
using Adm.Company.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Adm.Company.Api.Controllers;

[ApiController]
[Route("cliente")]
[AutenticaUsuario]
[AutenticaEmpresa]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _clienteService;

    public ClienteController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    [HttpGet("paginacao")]
    public async Task<IActionResult> Paginacao()
    {
        var clientes = await _clienteService.PaginacaoAsync();
        return Ok(clientes);
    }
}
