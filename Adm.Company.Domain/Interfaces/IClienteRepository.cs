using Adm.Company.Domain.Entities;

namespace Adm.Company.Domain.Interfaces;

public interface IClienteRepository
{
    Task AddAsync(Cliente cliente);
    Task AddRangeAsync(IList<Cliente> clientes);
    Task UpdateRangeAsync(IList<Cliente> clientes);
    Task<Cliente?> GetByNumeroWhatsAsync(string numeroWhats, Guid empresaId);
    Task<Cliente?> GetByRemoteJidWhatsAsync(string remoteJid, Guid empresaId);
    Task<Cliente?> GetByIdAsync(Guid id, Guid empresaId);
    Task<IList<Cliente>> GetPaginacaoAsync(Guid empresaId, int skip, string? search);
}
