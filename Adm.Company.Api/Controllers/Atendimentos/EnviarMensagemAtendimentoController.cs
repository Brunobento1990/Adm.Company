using Adm.Company.Api.Attributes;
using Adm.Company.Application.Dtos.Atendimentos;
using Adm.Company.Application.Interfaces.Atendimento;
using Microsoft.AspNetCore.Mvc;

namespace Adm.Company.Api.Controllers.Atendimentos;

[ApiController]
[Route("enviar-mensagem")]
[AutenticaUsuario]
[AutenticaEmpresa]
public class EnviarMensagemAtendimentoController : ControllerBase
{
    private readonly IEnviarMensagemAtendimentoService _enviarMensagemAtendimentoService;

    public EnviarMensagemAtendimentoController(IEnviarMensagemAtendimentoService enviarMensagemAtendimentoService)
    {
        _enviarMensagemAtendimentoService = enviarMensagemAtendimentoService;
    }

    [HttpPost]
    public async Task<IActionResult> EnviarMensagem(EnviarMensagemAtendimentoDto enviarMensagemAtendimentoDto)
    {
        var response = await _enviarMensagemAtendimentoService.EnviarMensagemAsync(enviarMensagemAtendimentoDto);
        return Ok(response);
    }
}
