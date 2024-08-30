using Adm.Company.Domain.Entities;
using Adm.Company.Domain.Enums;

namespace Adm.Company.Domain.Interfaces;

public interface IAtendimentoRepository
{
    Task UpdateAsync(Atendimento atendimento);
    Task<Atendimento?> GetAtendimentoByStatusAsync(
        StatusAtendimento statusAtendimento, 
        StatusAtendimento statusAtendimentoOutro, 
        string numeroWhats, 
        Guid empresaId);
    Task AddAsync(Atendimento atendimento);
    Task<Atendimento?> GetByIdAsync(Guid id);
    Task<IList<Atendimento>> GetMeuAtendimentosAsync(Guid usuarioId, Guid empresaId, StatusAtendimento statusAtendimento);
    Task<IList<Atendimento>> GetAtendimentosAsync(Guid empresaId, StatusAtendimento statusAtendimento);
}
