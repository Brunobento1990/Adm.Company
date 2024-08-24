using Adm.Company.Application.ViewModel.Atendimentos;

namespace Adm.Company.Application.Interfaces.Atendimento;

public interface IAtendimentoService
{
    Task<IList<AtendimentoViewModel>> MeusAtendimentosAsync();
    Task<IList<AtendimentoViewModel>> AtendimentosEmAbertoAsync();
    Task<AtendimentoViewModel> IniciarAtendimentoAsync(Guid atendimentoId);
}
