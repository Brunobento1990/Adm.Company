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

    public async Task<Cliente?> GetByNumeroWhatsAsync(string numeroWhats)
    {
        return await _admCompanyContext
            .Clientes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.WhatsApp == numeroWhats);
    }
}
