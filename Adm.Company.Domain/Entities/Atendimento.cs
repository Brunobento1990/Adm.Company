using Adm.Company.Domain.Enums;
using static Adm.Company.Domain.Entities.MensagemAtendimento;

namespace Adm.Company.Domain.Entities;

public sealed class Atendimento : BaseEntityEmpresa
{
    public Atendimento(
        Guid id,
        DateTime criadoEm,
        DateTime? atualizadoEm,
        long numero,
        Guid empresaId,
        StatusAtendimento status,
        Guid? usuarioId,
        Guid? usuarioCancelamentoId,
        string? observacao,
        string? mensagemCancelamento,
        Guid clienteId)
            : base(id, criadoEm, atualizadoEm, numero, empresaId)
    {
        Status = status;
        UsuarioId = usuarioId;
        UsuarioCancelamentoId = usuarioCancelamentoId;
        Observacao = observacao;
        MensagemCancelamento = mensagemCancelamento;
        ClienteId = clienteId;
    }

    public StatusAtendimento Status { get; private set; }
    public Guid? UsuarioId { get; private set; }
    public Usuario? Usuario { get; set; }
    public Guid? UsuarioCancelamentoId { get; private set; }
    public Usuario? UsuarioCancelamento { get; set; }
    public string? Observacao { get; private set; }
    public string? MensagemCancelamento { get; private set; }
    public IList<MensagemAtendimento> Mensagens { get; set; } = [];
    public Guid ClienteId { get; private set; }
    public Cliente Cliente { get; set; } = null!;

    public void IniciarAtendimento(Guid usuarioId)
    {
        UsuarioId = usuarioId;
        Status = StatusAtendimento.EmAndamento;
    }
    public static class Factorie
    {
        public static Atendimento Iniciar(
            string mensagem,
            Guid empresaId,
            string tipoMensagem,
            string remoteId,
            bool minhaMensagem,
            Guid clienteId,
            byte[]? audio,
            byte[]? figurinha,
            byte[]? imagem,
            string? descricaoFoto)
        {
            var atendimento = new Atendimento(
                id: Guid.NewGuid(),
                criadoEm: DateTime.Now,
                atualizadoEm: DateTime.Now,
                numero: 0,
                empresaId: empresaId,
                status: StatusAtendimento.Aberto,
                usuarioId: null,
                usuarioCancelamentoId: null,
                observacao: null,
                mensagemCancelamento: null,
                clienteId: clienteId);

            atendimento.Mensagens.Add(FabricaMensagem.Fabricar(
                mensagem: mensagem,
                minhaMensagem: minhaMensagem,
                remoteId: remoteId,
                atendimentoId: atendimento.Id,
                audio: audio,
                status: StatusMensagem.Entregue,
                figurinha: figurinha,
                imagem: imagem,
                descricaoFoto: descricaoFoto));

            return atendimento;
        }
    }
}
