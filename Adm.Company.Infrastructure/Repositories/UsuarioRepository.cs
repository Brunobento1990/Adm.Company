using Adm.Company.Domain.Entities;
using Adm.Company.Domain.Interfaces;
using Adm.Company.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Adm.Company.Infrastructure.Repositories;

public sealed class UsuarioRepository : IUsuarioRepository
{
    private readonly AdmCompanyContext _admCompanyContext;

    public UsuarioRepository(AdmCompanyContext admCompanyContext)
    {
        _admCompanyContext = admCompanyContext;
    }

    public async Task<Usuario?> GetByIdAsync(Guid id)
    {
        return await _admCompanyContext
            .Usuarios
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
