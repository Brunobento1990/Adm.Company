using Adm.Company.Domain.Entities;
using Adm.Company.Domain.Interfaces;
using Adm.Company.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Adm.Company.Infrastructure.Repositories;

public sealed class MensagemAtendimentoRepository : IMensagemAtendimentoRepository
{
    private readonly AdmCompanyContext _admCompanyContext;

    public MensagemAtendimentoRepository(AdmCompanyContext admCompanyContext)
    {
        _admCompanyContext = admCompanyContext;
    }

    public async Task AddAsync(MensagemAtendimento mensagemAtendimento)
    {
        await _admCompanyContext.AddAsync(mensagemAtendimento);
        await _admCompanyContext.SaveChangesAsync();
    }

    public async Task<MensagemAtendimento?> GetByRemoteIdAsync(string remoteId)
    {
        return await _admCompanyContext
            .MensagemAtendimentos
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.RemoteId == remoteId);
    }

    public async Task UpdateAsync(MensagemAtendimento mensagemAtendimento)
    {
        _admCompanyContext.Attach(mensagemAtendimento);
        _admCompanyContext.Update(mensagemAtendimento);
        await _admCompanyContext.SaveChangesAsync();
    }
}
