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
        Guid atendimentoId,
        StatusMensagem status,
        string tipoMensagem,
        string remoteId,
        bool minhaMensagem)
            : base(id, criadoEm, atualizadoEm, numero)
    {
        Mensagem = mensagem;
        AtendimentoId = atendimentoId;
        Status = status;
        TipoMensagem = tipoMensagem;
        RemoteId = remoteId;
        MinhaMensagem = minhaMensagem;
    }

    public void UpdateStatus(StatusMensagem statusMensagem)
    {
        Status = statusMensagem;
    }

    public StatusMensagem Status { get; private set; }
    public string Mensagem { get; private set; }
    public string TipoMensagem { get; private set; }
    public string RemoteId { get; private set; }
    public bool MinhaMensagem { get; private set; }
    public Guid AtendimentoId { get; private set; }
    public Atendimento Atendimento { get; set; } = null!;
}
