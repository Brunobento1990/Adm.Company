using Adm.Company.Domain.Entities;
using Adm.Company.Domain.Interfaces;
using Adm.Company.Infrastructure.Context;
using Adm.Company.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Adm.Company.Infrastructure.Repositories;

public sealed class ClienteRepository : IClienteRepository
{
    private readonly AdmCompanyContext _admCompanyContext;

    public ClienteRepository(AdmCompanyContext admCompanyContext)
    {
        _admCompanyContext = admCompanyContext;
    }

    public async Task AddAsync(Cliente cliente)
    {
        await _admCompanyContext.AddAsync(cliente);
        await _admCompanyContext.SaveChangesAsync();
    }

    public async Task AddRangeAsync(IList<Cliente> clientes)
    {
        if (clientes?.Count > 0)
        {
            await _admCompanyContext.AddRangeAsync(clientes);
            await _admCompanyContext.SaveChangesAsync();
        }

    }

    public async Task<Cliente?> GetByIdAsync(Guid id, Guid empresaId)
    {
        return await _admCompanyContext
            .Clientes
            .FirstOrDefaultAsync(x => x.Id == id && x.EmpresaId == empresaId);
    }

    public async Task<Cliente?> GetByNumeroWhatsAsync(string numeroWhats, Guid empresaId)
    {
        return await _admCompanyContext
            .Clientes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.WhatsApp == numeroWhats && x.EmpresaId == empresaId);
    }

    public async Task<Cliente?> GetByRemoteJidWhatsAsync(string remoteJid, Guid empresaId)
    {
        return await _admCompanyContext
            .Clientes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.RemoteJid == remoteJid && x.EmpresaId == empresaId);
    }

    public async Task<IList<Cliente>> GetPaginacaoAsync(Guid empresaId, int skip, string? search)
    {
        var clientes = _admCompanyContext
            .Clientes
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            clientes = clientes.Where(x => x.EmpresaId == empresaId && x.Nome.ToLower().Contains(search.ToLower()));
        }
        else
        {
            clientes = clientes.Where(x => x.EmpresaId == empresaId);
        };


        return await clientes  
            .OrderBy(x => x.Nome)
            .Paginate(skip: skip, take: 50)
            .ToListAsync();
    }

    public async Task UpdateRangeAsync(IList<Cliente> clientes)
    {
        if (clientes?.Count > 0)
        {
            _admCompanyContext.AttachRange(clientes);
            _admCompanyContext.UpdateRange(clientes);
            await _admCompanyContext.SaveChangesAsync();
        }
    }
}
