using Adm.Company.Application.Interfaces.Atendimento;
using Adm.Company.Application.ViewModel.Atendimentos;
using Adm.Company.Domain.Interfaces;

namespace Adm.Company.Application.Services.Atendimentos;

public sealed class MensagemAtendimentoService : IMensagemAtendimentoService
{
    private readonly IMensagemAtendimentoRepository _mensagemAtendimentoRepository;

    public MensagemAtendimentoService(IMensagemAtendimentoRepository mensagemAtendimentoRepository)
    {
        _mensagemAtendimentoRepository = mensagemAtendimentoRepository;
    }

    public async Task<IList<MensagemAtendimentoViewModel>> MensagensDoAtendimentoAsync(Guid atendimentoId)
    {
        var mensagensAtendimentos = await _mensagemAtendimentoRepository
            .MensagensDoAtendimentoAsync(atendimentoId);

        return mensagensAtendimentos.Select(x => (MensagemAtendimentoViewModel)x).ToList();
    }
}
