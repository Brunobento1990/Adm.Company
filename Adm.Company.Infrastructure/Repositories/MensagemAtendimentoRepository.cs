using Adm.Company.Domain.Entities;
using Adm.Company.Domain.Enums;
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

    public async Task BulkUpdateStatusAsync(IList<Guid> ids, StatusMensagem status)
    {
        if (ids.Count == 0) return;

        await _admCompanyContext
            .MensagemAtendimentos
            .Where(x => ids.Contains(x.Id))
            .ExecuteUpdateAsync(x => x.SetProperty(y => y.Status, status));
    }

    public async Task<MensagemAtendimento?> GetByRemoteIdAsync(string remoteId)
    {
        return await _admCompanyContext
            .MensagemAtendimentos
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.RemoteId == remoteId);
    }

    public async Task<IList<MensagemAtendimento>> MensagensDoAtendimentoAsync(Guid atendimentoId)
    {
        return await _admCompanyContext
            .MensagemAtendimentos
            .AsNoTracking()
            .OrderBy(x => x.AtualizadoEm)
            .Where(x => x.AtendimentoId == atendimentoId)
            .Take(100)
            .ToListAsync();
    }

    public async Task<int> MensagensNaoLidasAtendimentoAsync(Guid atendimentoId)
    {
        try
        {
            return await _admCompanyContext
                .MensagemAtendimentos
                .CountAsync(x => x.Status != Domain.Enums.StatusMensagem.Lida && !x.MinhaMensagem && x.AtendimentoId == atendimentoId);
        }
        catch (Exception)
        {
            return 0;
        }
    }

    public async Task UpdateAsync(MensagemAtendimento mensagemAtendimento)
    {
        _admCompanyContext.Attach(mensagemAtendimento);
        _admCompanyContext.Update(mensagemAtendimento);
        await _admCompanyContext.SaveChangesAsync();
    }
}
