using Adm.Company.Api.Attributes;
using Adm.Company.Application.Dtos.Clientes;
using Adm.Company.Application.Interfaces;
using Adm.Company.Application.ViewModel.WhatsApi;
using Adm.Company.Application.ViewModel;
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
    [ProducesResponseType<IList<ClienteViewModel>>(200)]
    [ProducesResponseType<ErrorResponse>(401)]
    [ProducesResponseType<ErrorResponse>(400)]
    public async Task<IActionResult> Paginacao([FromQuery] PaginacaoClienteWhatsDto paginacaoClienteWhatsDto)
    {
        var clientes = await _clienteService.PaginacaoAsync(paginacaoClienteWhatsDto);
        return Ok(clientes);
    }
}
