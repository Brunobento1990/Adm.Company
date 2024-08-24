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
        bool minhaMensagem,
        byte[]? audio)
            : base(id, criadoEm, atualizadoEm, numero)
    {
        Mensagem = mensagem;
        AtendimentoId = atendimentoId;
        Status = status;
        TipoMensagem = tipoMensagem;
        RemoteId = remoteId;
        MinhaMensagem = minhaMensagem;
        Audio = audio;
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
    public byte[]? Audio { get; private set; }

    public class FabricaMensagem
    {
        public static MensagemAtendimento Fabricar(
            string mensagem,
            bool minhaMensagem,
            string remoteId,
            Guid atendimentoId,
            byte[]? audio,
            StatusMensagem status)
        {
            string tipoMensagem = "conversation";

            if (audio != null)
            {
                tipoMensagem = "audioMessage";
            }

            return new MensagemAtendimento(
                id: Guid.NewGuid(),
                criadoEm: DateTime.Now,
                atualizadoEm: DateTime.Now,
                numero: 0,
                mensagem: mensagem,
                atendimentoId: atendimentoId,
                status: status,
                tipoMensagem: tipoMensagem,
                remoteId: remoteId,
                minhaMensagem: minhaMensagem,
                audio: audio);
        }
    }
}
