using Adm.Company.Domain.Entities;

namespace Adm.Company.Domain.Interfaces;

public interface IEmpresaRepository
{
    Task<Empresa?> GetEmpresaByIdAsync(Guid id);
}
