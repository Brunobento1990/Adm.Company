using Adm.Company.Application.Dtos.ConfiguracoesAtendimentoEmpresa;
using Adm.Company.Application.ViewModel;

namespace Adm.Company.Application.Interfaces;

public interface IConfiguracaoAtendimentoEmpresaService
{
    Task<ConfiguracaoAtendimentoEmpresaViewModel> CreateOrUpdateAsync(ConfiguracaoAtendimentoEmpresaDto configuracaoAtendimentoEmpresaDto);
    Task<ConfiguracaoAtendimentoEmpresaViewModel> GetAsync();
}
