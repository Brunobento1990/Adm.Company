using Adm.Company.Domain.Entities;

namespace Adm.Company.Domain.Interfaces;

public interface IConfiguracaoAtendimentoEmpresaRepository
{
    Task<ConfiguracaoAtendimentoEmpresa?> GetConfiguracaoAtendimentoEmpresaByEmpresaIdAsync(Guid empresaId);
}
