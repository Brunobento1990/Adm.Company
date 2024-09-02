using Adm.Company.Domain.Entities;
using Adm.Company.Domain.Interfaces;
using Adm.Company.Infrastructure.Context;
using Adm.Company.Infrastructure.Extensions;
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

    public async Task<IList<Usuario>> GetPaginacaoAsync(Guid empresaId, int skip, string? search)
    {
        var usuarios = _admCompanyContext
            .Usuarios
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            usuarios = usuarios.Where(x => x.EmpresaId == empresaId && x.Nome.ToLower().Contains(search.ToLower()));
            skip = 1;
        }
        else
        {
            usuarios = usuarios.Where(x => x.EmpresaId == empresaId);
        };


        return await usuarios
            .OrderBy(x => x.Nome)
            .Paginate(skip: skip, take: 50)
            .ToListAsync();
    }
}
