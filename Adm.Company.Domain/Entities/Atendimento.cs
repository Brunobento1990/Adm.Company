using Adm.Company.Domain.Enums;
using Adm.Company.Domain.Exceptions;
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
        string? motivoCancelamento,
        Guid clienteId,
        Guid? usuarioFinalizadoId)
            : base(id, criadoEm, atualizadoEm, numero, empresaId)
    {
        Status = status;
        UsuarioId = usuarioId;
        UsuarioCancelamentoId = usuarioCancelamentoId;
        Observacao = observacao;
        MotivoCancelamento = motivoCancelamento;
        ClienteId = clienteId;
        UsuarioFinalizadoId = usuarioFinalizadoId;
    }

    public StatusAtendimento Status { get; private set; }
    public Guid? UsuarioId { get; private set; }
    public Usuario? Usuario { get; set; }
    public Guid? UsuarioCancelamentoId { get; private set; }
    public Usuario? UsuarioCancelamento { get; set; }
    public Guid? UsuarioFinalizadoId { get; private set; }
    public Usuario? UsuarioFinalizado { get; set; }
    public string? Observacao { get; private set; }
    public string? MotivoCancelamento { get; private set; }
    public IList<MensagemAtendimento> Mensagens { get; set; } = [];
    public Guid ClienteId { get; private set; }
    public Cliente Cliente { get; set; } = null!;

    public void IniciarAtendimento(Guid usuarioId)
    {
        UsuarioId = usuarioId;
        Status = StatusAtendimento.EmAndamento;
    }

    public void CancelarAtendimento(Guid usuarioId, string motivoCancelamento, string? observacao)
    {
        if (string.IsNullOrWhiteSpace(motivoCancelamento))
        {
            throw new ExceptionApiErro("Informe o motivo do cancelamento!");
        }

        UsuarioCancelamentoId = usuarioId;
        MotivoCancelamento = motivoCancelamento;
        Status = StatusAtendimento.Cancelado;
        Observacao = observacao;
    }

    public void FinalizarAtendimento(Guid usuarioId, string? observacao)
    {
        UsuarioFinalizadoId = usuarioId;
        Observacao = observacao;
        Status = StatusAtendimento.Fechado;
    }

    public static class Factorie
    {
        public static Atendimento NovoAtendimento(Guid empresaId, Guid clienteId, Guid usuarioId)
        {
            return new Atendimento(
                id: Guid.NewGuid(),
                criadoEm: DateTime.Now,
                atualizadoEm: null,
                numero: 0,
                empresaId: empresaId,
                status: StatusAtendimento.EmAndamento,
                usuarioId: usuarioId,
                usuarioCancelamentoId: null,
                observacao: null,
                motivoCancelamento: null,
                clienteId: clienteId,
                usuarioFinalizadoId: null);
        }

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
            string? descricaoFoto,
            Guid? usuarioId)
        {
            var atendimento = new Atendimento(
                id: Guid.NewGuid(),
                criadoEm: DateTime.Now,
                atualizadoEm: DateTime.Now,
                numero: 0,
                empresaId: empresaId,
                status: usuarioId.HasValue ? StatusAtendimento.EmAndamento : StatusAtendimento.Aberto,
                usuarioId: usuarioId,
                usuarioCancelamentoId: null,
                observacao: null,
                motivoCancelamento: null,
                clienteId: clienteId,
                usuarioFinalizadoId: null);

            atendimento.Mensagens.Add(FabricaMensagem.Fabricar(
                mensagem: mensagem,
                minhaMensagem: minhaMensagem,
                remoteId: remoteId,
                atendimentoId: atendimento.Id,
                audio: audio,
                status: StatusMensagem.Entregue,
                figurinha: figurinha,
                imagem: imagem,
                descricaoFoto: descricaoFoto,
                resposta: null,
                respostaId: null));

            return atendimento;
        }
    }
}
