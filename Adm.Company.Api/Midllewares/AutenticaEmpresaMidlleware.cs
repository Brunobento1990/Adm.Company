using Adm.Company.Api.Attributes;
using Adm.Company.Domain.Exceptions;
using Adm.Company.Domain.Interfaces;
using Microsoft.AspNetCore.Http.Features;

namespace Adm.Company.Api.Midllewares;

public class AutenticaEmpresaMidlleware
{
    private readonly RequestDelegate _next;
    public AutenticaEmpresaMidlleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(
        HttpContext httpContext,
        IUsuarioAutenticado usuarioAutenticado,
        IEmpresaAutenticada empresaAutenticada,
        IEmpresaRepository empresaRepository)
    {

        var autenticar = httpContext.Features.Get<IEndpointFeature>()?.Endpoint?.Metadata
                .FirstOrDefault(m => m is AutenticaEmpresaAttribute) is AutenticaEmpresaAttribute;

        if (!autenticar)
        {
            await _next(httpContext);
            return;
        }

        var empresa = await empresaRepository.GetEmpresaByIdAsync(usuarioAutenticado.EmpresaId)
            ?? throw new ExceptionApiUnauthorized("Não foi possível autenticar a empresa");

        empresaAutenticada.Id = empresa.Id;

        await _next(httpContext);
    }
}
