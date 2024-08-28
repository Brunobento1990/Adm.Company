using Adm.Company.Domain.Entities;
using Adm.Company.Domain.Interfaces;
using Adm.Company.Infrastructure.Context;
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

    public async Task UpdateRangeAsync(IList<Cliente> clientes)
    {
        if(clientes?.Count > 0)
        {
            _admCompanyContext.AttachRange(clientes);
            _admCompanyContext.UpdateRange(clientes);
            await _admCompanyContext.SaveChangesAsync();
        }
    }
}
