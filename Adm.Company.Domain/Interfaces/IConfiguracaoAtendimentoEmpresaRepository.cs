using Adm.Company.Domain.Entities;

namespace Adm.Company.Domain.Interfaces;

public interface IConfiguracaoAtendimentoEmpresaRepository
{
    Task<ConfiguracaoAtendimentoEmpresa?> GetConfiguracaoAtendimentoEmpresaByEmpresaIdAsync(Guid empresaId);
    Task<ConfiguracaoAtendimentoEmpresa?> GetConfiguracaoAtendimentoEmpresaByNumeroWhtasAsync(string numeroWhats);
    Task AddAsync(ConfiguracaoAtendimentoEmpresa configuracaoAtendimentoEmpresa);
    Task UpdateAsync(ConfiguracaoAtendimentoEmpresa configuracaoAtendimentoEmpresa);
}
