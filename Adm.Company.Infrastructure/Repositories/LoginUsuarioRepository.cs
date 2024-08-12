using Adm.Company.Domain.Entities;
using Adm.Company.Domain.Interfaces;
using Adm.Company.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Adm.Company.Infrastructure.Repositories;

public sealed class LoginUsuarioRepository : ILoginUsuarioRepository
{
    private readonly AdmCompanyContext _admCompanyContext;

    public LoginUsuarioRepository(AdmCompanyContext admCompanyContext)
    {
        _admCompanyContext = admCompanyContext;
    }

    public async Task<Usuario?> LoginAsync(string email)
    {
        return await _admCompanyContext
            .Usuarios
            .AsNoTracking()
            .Include(x => x.Empresa)
            .FirstOrDefaultAsync(x => x.Email == email);
    }
}
