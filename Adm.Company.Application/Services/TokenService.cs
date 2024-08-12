using Adm.Company.Application.Interfaces;
using Adm.Company.Application.ViewModel.Jwt;
using Adm.Company.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Adm.Company.Application.Services;

public sealed class TokenService : ITokenService
{
    public string GetToken(Usuario usuario)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(ConfiguracaoJwt.Key));

        var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
          issuer: ConfiguracaoJwt.Issue,
          audience: ConfiguracaoJwt.Audience,
          claims: GenerateClaims(usuario),
          expires: DateTime.Now.AddHours(ConfiguracaoJwt.Expiration),
          signingCredentials: credenciais);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public Claim[] GenerateClaims(object obj)
    {
        var claims = new List<Claim>();

        foreach (var property in obj.GetType().GetProperties())
        {
            var value = property.GetValue(obj);
            if (value != null)
                claims.Add(new Claim(property.Name, value.ToString() ?? "Sem Valor"));

        }

        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

        return [.. claims];
    }
}
