using Adm.Company.Domain.Entities;

namespace Adm.Company.Domain.Interfaces;

public interface ILoginUsuarioRepository
{
    Task<Usuario?> LoginAsync(string email);
}
