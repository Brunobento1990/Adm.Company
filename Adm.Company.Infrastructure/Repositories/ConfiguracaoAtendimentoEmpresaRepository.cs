using Adm.Company.Domain.Entities;
using Adm.Company.Domain.Interfaces;
using Adm.Company.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Adm.Company.Infrastructure.Repositories;

public sealed class ConfiguracaoAtendimentoEmpresaRepository : IConfiguracaoAtendimentoEmpresaRepository
{
    private readonly AdmCompanyContext _admCompanyContext;

    public ConfiguracaoAtendimentoEmpresaRepository(AdmCompanyContext admCompanyContext)
    {
        _admCompanyContext = admCompanyContext;
    }

    public async Task AddAsync(ConfiguracaoAtendimentoEmpresa configuracaoAtendimentoEmpresa)
    {
        await _admCompanyContext.AddAsync(configuracaoAtendimentoEmpresa);
        await _admCompanyContext.SaveChangesAsync();
    }

    public async Task<ConfiguracaoAtendimentoEmpresa?> GetConfiguracaoAtendimentoEmpresaByEmpresaIdAsync(Guid empresaId)
    {
        return await _admCompanyContext
            .ConfiguracaoAtendimentoEmpresa
            .AsNoTracking()
            .OrderByDescending(x => x.CriadoEm)
            .FirstOrDefaultAsync(x => x.EmpresaId == empresaId);
            
    }

    public async Task<ConfiguracaoAtendimentoEmpresa?> GetConfiguracaoAtendimentoEmpresaByNumeroWhtasAsync(string numeroWhats)
    {
        return await _admCompanyContext
            .ConfiguracaoAtendimentoEmpresa
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.WhatsApp ==  numeroWhats);
    }

    public async Task UpdateAsync(ConfiguracaoAtendimentoEmpresa configuracaoAtendimentoEmpresa)
    {
        _admCompanyContext.Update(configuracaoAtendimentoEmpresa);
        await _admCompanyContext.SaveChangesAsync();
    }
}
