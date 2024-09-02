using Adm.Company.Domain.Entities;
using Adm.Company.Domain.Enums;

namespace Adm.Company.Domain.Interfaces;

public interface IMensagemAtendimentoRepository
{
    Task AddAsync(MensagemAtendimento mensagemAtendimento);
    Task<MensagemAtendimento?> GetByRemoteIdAsync(string remoteId);
    Task<MensagemAtendimento?> GetByIdAsync(Guid id);
    Task UpdateAsync(MensagemAtendimento mensagemAtendimento);
    Task<int> MensagensNaoLidasAtendimentoAsync(Guid atendimentoId);
    Task<IList<MensagemAtendimento>> MensagensDoAtendimentoAsync(Guid atendimentoId);
    Task BulkUpdateStatusAsync(IList<Guid> ids, StatusMensagem statusMensagem);
}
