using Adm.Company.Application.Interfaces.Atendimento;
using Adm.Company.Application.ViewModel.Atendimentos;
using Adm.Company.Domain.Enums;
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

        var idsMensagensNaoLidas = mensagensAtendimentos
            .Where(x => !x.MinhaMensagem && x.Status != StatusMensagem.Lida)
            .Select(x => x.Id)
            .ToList();
        await _mensagemAtendimentoRepository
            .BulkUpdateStatusAsync(idsMensagensNaoLidas, StatusMensagem.Lida);
        
        var lerMensagens = mensagensAtendimentos
            .Where(x => !x.MinhaMensagem && x.Status != StatusMensagem.Lida)
            .ToList();

        foreach (var mensagem in lerMensagens)
        {
            mensagem.UpdateStatus(StatusMensagem.Lida);
        }

        return mensagensAtendimentos.Select(x => (MensagemAtendimentoViewModel)x).ToList();
    }
}
