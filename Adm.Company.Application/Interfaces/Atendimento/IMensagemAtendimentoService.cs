using Adm.Company.Application.ViewModel.Atendimentos;

namespace Adm.Company.Application.Interfaces.Atendimento;

public interface IMensagemAtendimentoService
{
    Task<IList<MensagemAtendimentoViewModel>> MensagensDoAtendimentoAsync(Guid atendimentoId);
}
