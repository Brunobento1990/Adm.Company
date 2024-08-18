using Adm.Company.Domain.Entities;

namespace Adm.Company.Domain.Interfaces;

public interface IClienteRepository
{
    Task AddAsync(Cliente cliente);
    Task<Cliente?> GetByNumeroWhatsAsync(string numeroWhats);    
}
