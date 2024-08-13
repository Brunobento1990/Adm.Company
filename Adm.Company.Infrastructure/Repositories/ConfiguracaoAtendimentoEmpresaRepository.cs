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

    public async Task<ConfiguracaoAtendimentoEmpresa?> GetConfiguracaoAtendimentoEmpresaByEmpresaIdAsync(Guid empresaId)
    {
        return await _admCompanyContext
            .ConfiguracaoAtendimentoEmpresa
            .AsNoTracking()
            .OrderByDescending(x => x.CriadoEm)
            .FirstOrDefaultAsync(x => x.EmpresaId == empresaId);
            
    }
}
