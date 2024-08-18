using Adm.Company.Domain.Enums;

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

    public static class Factorie
    {
        public static Atendimento Iniciar(
            string mensagem, 
            Guid empresaId, 
            string tipoMensagem, 
            string remoteId,
            bool minhaMensagem,
            Guid clienteId)
        {
            var atendimento = new Atendimento(
                id: Guid.NewGuid(),
                criadoEm: DateTime.Now,
                atualizadoEm: null,
                numero: 0,
                empresaId: empresaId,
                status: StatusAtendimento.Aberto,
                usuarioId: null,
                usuarioCancelamentoId: null,
                observacao: null,
                mensagemCancelamento: null,
                clienteId: clienteId);

            atendimento.Mensagens.Add(new MensagemAtendimento(
                id: Guid.NewGuid(),
                criadoEm: DateTime.Now,
                atualizadoEm: DateTime.Now,
                numero: 0,
                mensagem: mensagem,
                status: StatusMensagem.Entregue,
                atendimentoId: atendimento.Id,
                tipoMensagem: tipoMensagem,
                remoteId: remoteId,
                minhaMensagem: minhaMensagem));

            return atendimento;
        }
    }
}
