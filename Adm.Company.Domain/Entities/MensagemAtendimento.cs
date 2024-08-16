using Adm.Company.Domain.Enums;

namespace Adm.Company.Domain.Entities;

public sealed class MensagemAtendimento : BaseEntity
{
    public MensagemAtendimento(
        Guid id,
        DateTime criadoEm,
        DateTime? atualizadoEm,
        long numero,
        string mensagem,
        Guid atendimentoId)
            : base(id, criadoEm, atualizadoEm, numero)
    {
        Mensagem = mensagem;
        AtendimentoId = atendimentoId;
    }

    public StatusMensagem Status { get; private set; }
    public string Mensagem { get; private set; }
    public Guid AtendimentoId { get; private set; }
    public Atendimento Atendimento { get; set; } = null!;
}
