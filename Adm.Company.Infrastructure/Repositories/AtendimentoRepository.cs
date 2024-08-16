using Adm.Company.Domain.Entities;
using Adm.Company.Domain.Enums;
using Adm.Company.Domain.Interfaces;
using Adm.Company.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Adm.Company.Infrastructure.Repositories;

public sealed class AtendimentoRepository : IAtendimentoRepository
{
    private readonly AdmCompanyContext _admCompanyContext;

    public AtendimentoRepository(AdmCompanyContext admCompanyContext)
    {
        _admCompanyContext = admCompanyContext;
    }

    public async Task AddAsync(Atendimento atendimento)
    {
        await _admCompanyContext.AddAsync(atendimento);
        await _admCompanyContext.SaveChangesAsync();
    }

    public async Task<Atendimento?> GetAtendimentoByStatusAsync(StatusAtendimento statusAtendimento, string numeroWhats, Guid empresaId)
    {
        return await _admCompanyContext
            .Atendimentos
            .AsNoTracking()
            .Include(x => x.Mensagens)
            .FirstOrDefaultAsync(x => x.Status == statusAtendimento && x.NumeroWhats == numeroWhats && x.EmpresaId == empresaId);
    }
}
