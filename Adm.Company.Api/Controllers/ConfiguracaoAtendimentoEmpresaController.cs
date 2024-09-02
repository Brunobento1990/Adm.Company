using Adm.Company.Api.Attributes;
using Adm.Company.Application.Dtos.ConfiguracoesAtendimentoEmpresa;
using Adm.Company.Application.Interfaces;
using Adm.Company.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Adm.Company.Api.Controllers;

[ApiController]
[Route("configuracao-atendimento-empresa")]
[AutenticaUsuario]
[AutenticaEmpresa]
public class ConfiguracaoAtendimentoEmpresaController : ControllerBase
{
    private readonly IConfiguracaoAtendimentoEmpresaService _configuracaoAtendimentoEmpresaService;

    public ConfiguracaoAtendimentoEmpresaController(IConfiguracaoAtendimentoEmpresaService configuracaoAtendimentoEmpresaService)
    {
        _configuracaoAtendimentoEmpresaService = configuracaoAtendimentoEmpresaService;
    }

    [HttpGet]
    [ProducesResponseType<ConfiguracaoAtendimentoEmpresaViewModel>(200)]
    [ProducesResponseType<ErrorResponse>(401)]
    [ProducesResponseType<ErrorResponse>(400)]
    public async Task<IActionResult> GetConfig()
    {
        var config = await _configuracaoAtendimentoEmpresaService.GetAsync();
        return Ok(config);
    }

    [HttpPut]
    [ProducesResponseType<ConfiguracaoAtendimentoEmpresaViewModel>(200)]
    [ProducesResponseType<ErrorResponse>(401)]
    [ProducesResponseType<ErrorResponse>(400)]
    public async Task<IActionResult> Update(ConfiguracaoAtendimentoEmpresaDto configuracaoAtendimentoEmpresaDto)
    {
        var result = await _configuracaoAtendimentoEmpresaService.CreateOrUpdateAsync(configuracaoAtendimentoEmpresaDto);
        return Ok(result);
    }
}
