using Adm.Company.Api.Attributes;
using Adm.Company.Api.Configurations;
using Adm.Company.Application.ViewModel.Jwt;
using Adm.Company.Domain.Exceptions;
using Adm.Company.Domain.Interfaces;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Adm.Company.Api.Midllewares;

public class AutenticaUsuarioMidlleware
{
    private readonly RequestDelegate _next;
    public AutenticaUsuarioMidlleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(
        HttpContext httpContext,
        IUsuarioAutenticado usuarioAutenticado)
    {

        var autenticar = httpContext.Features.Get<IEndpointFeature>()?.Endpoint?.Metadata
                .FirstOrDefault(m => m is AutenticaUsuarioAttribute) is AutenticaUsuarioAttribute;

        if (!autenticar)
        {
            await _next(httpContext);
            return;
        }

        var token = httpContext.Request.Headers.Authorization.ToString().Split(" ")?.Last();

        if (string.IsNullOrWhiteSpace(token))
        {
            throw new ExceptionApiUnauthorized("Jwt inválido!");
        }

        var keyJwt = VariaveisDeAmbiente.GetVariavel("JWT_KEY");

        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = ConfiguracaoJwt.Issue,
                ValidAudience = ConfiguracaoJwt.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfiguracaoJwt.Key))
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;

            var id = jwtToken.Claims.FirstOrDefault(c => c.Type == "Id")?.Value
                ?? throw new ExceptionApiUnauthorized("Token inválido");
            var cpf = jwtToken.Claims.FirstOrDefault(c => c.Type == "Cpf")?.Value;
            var empresaId = jwtToken.Claims.FirstOrDefault(c => c.Type == "EmpresaId")?.Value;

            if (!Guid.TryParse(id, out Guid idParse) 
                || string.IsNullOrWhiteSpace(cpf) ||
                !Guid.TryParse(empresaId, out Guid empresaIdParse))
            {
                throw new ExceptionApiUnauthorized("Por favor, efetue o login novamente");
            }

            usuarioAutenticado.Id = idParse;
            usuarioAutenticado.Cpf = cpf;
            usuarioAutenticado.EmpresaId = empresaIdParse;

        }
        catch (SecurityTokenExpiredException)
        {
            throw new ExceptionApiUnauthorized("Sessão expirada, efetue o login novamente!");
        }
        catch (Exception)
        {
            throw new ExceptionApiUnauthorized("Efetue o login novamente!");
        }

        await _next(httpContext);
    }
}
