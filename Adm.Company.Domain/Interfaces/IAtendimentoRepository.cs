using Adm.Company.Domain.Entities;
using Adm.Company.Domain.Enums;

namespace Adm.Company.Domain.Interfaces;

public interface IAtendimentoRepository
{
    Task<Atendimento?> GetAtendimentoByStatusAsync(
        StatusAtendimento statusAtendimento, 
        string numeroWhats, 
        Guid empresaId);
    Task AddAsync(Atendimento atendimento);
    Task<Atendimento?> GetByIdAsync(Guid id);
    Task<IList<Atendimento>> GetMeuAtendimentosAsync(Guid usuarioId, Guid empresaId, StatusAtendimento statusAtendimento);
}
