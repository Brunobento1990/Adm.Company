using Adm.Company.Domain.Entities;
using Adm.Company.Domain.Interfaces;
using Adm.Company.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Adm.Company.Infrastructure.Repositories;

public sealed class EmpresaRepository : IEmpresaRepository
{
    private readonly AdmCompanyContext _admCompanyContext;

    public EmpresaRepository(AdmCompanyContext admCompanyContext)
    {
        _admCompanyContext = admCompanyContext;
    }

    public async Task<Empresa?> GetEmpresaByIdAsync(Guid id)
    {
        return await _admCompanyContext
            .Empresas
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
