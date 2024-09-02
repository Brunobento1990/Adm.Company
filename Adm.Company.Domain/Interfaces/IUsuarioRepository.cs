using Adm.Company.Domain.Entities;

namespace Adm.Company.Domain.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario?> GetByIdAsync(Guid id);
}
