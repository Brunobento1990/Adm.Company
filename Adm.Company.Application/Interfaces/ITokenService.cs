using Adm.Company.Domain.Entities;
using System.Security.Claims;

namespace Adm.Company.Application.Interfaces;

public interface ITokenService
{
    string GetToken(Usuario usuario);
    Claim[] GenerateClaims(object obj);
}
