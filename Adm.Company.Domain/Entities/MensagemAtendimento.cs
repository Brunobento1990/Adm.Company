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
        byte[]? audio,
        byte[]? imagem,
        byte[]? figurinha,
        string? descricaoFoto)
            : base(id, criadoEm, atualizadoEm, numero)
    {
        Mensagem = mensagem;
        AtendimentoId = atendimentoId;
        Status = status;
        TipoMensagem = tipoMensagem;
        RemoteId = remoteId;
        MinhaMensagem = minhaMensagem;
        Audio = audio;
        Imagem = imagem;
        Figurinha = figurinha;
        DescricaoFoto = descricaoFoto;
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
    public byte[]? Imagem { get; private set; }
    public byte[]? Figurinha { get; private set; }
    public string? DescricaoFoto { get; private set; }

    public class FabricaMensagem
    {
        public static MensagemAtendimento Fabricar(
            string mensagem,
            bool minhaMensagem,
            string remoteId,
            Guid atendimentoId,
            byte[]? audio,
            StatusMensagem status,
            byte[]? figurinha,
            byte[]? imagem,
            string? descricaoFoto)
        {
            string tipoMensagem = nameof(TipoMensagemEnum.conversation);

            if (audio != null)
            {
                tipoMensagem = nameof(TipoMensagemEnum.audioMessage);
            }

            if (figurinha != null)
            {
                tipoMensagem = nameof(TipoMensagemEnum.stickerMessage);
            }

            if (imagem != null)
            {
                tipoMensagem = nameof(TipoMensagemEnum.imageMessage);
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
                audio: audio,
                figurinha: figurinha,
                imagem: imagem,
                descricaoFoto: descricaoFoto);
        }
    }
}
