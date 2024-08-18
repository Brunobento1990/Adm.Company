using Adm.Company.Application.ViewModel.Atendimentos;

namespace Adm.Company.Application.Interfaces.Atendimento;

public interface IAtendimentoService
{
    Task<IList<AtendimentoViewModel>> MeusAtendimentosAsync();
}
