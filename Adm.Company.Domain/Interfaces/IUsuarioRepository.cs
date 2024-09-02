using Adm.Company.Domain.Entities;

namespace Adm.Company.Domain.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario?> GetByIdAsync(Guid id);
    Task<IList<Usuario>> GetPaginacaoAsync(Guid empresaId, int skip, string? search);
}
