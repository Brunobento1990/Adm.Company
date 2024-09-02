using Adm.Company.Application.ViewModel.Atendimentos;

namespace Adm.Company.Application.Interfaces.Atendimentos;

public interface IMensagemAtendimentoService
{
    Task<IList<MensagemAtendimentoViewModel>> MensagensDoAtendimentoAsync(Guid atendimentoId);
}
