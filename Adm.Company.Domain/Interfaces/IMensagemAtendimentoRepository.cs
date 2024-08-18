using Adm.Company.Domain.Entities;

namespace Adm.Company.Domain.Interfaces;

public interface IMensagemAtendimentoRepository
{
    Task AddAsync(MensagemAtendimento mensagemAtendimento);
    Task<MensagemAtendimento?> GetByRemoteIdAsync(string remoteId);
    Task UpdateAsync(MensagemAtendimento mensagemAtendimento);
}
