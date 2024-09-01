using Adm.Company.Domain.Entities;
using Adm.Company.Domain.Enums;

namespace Adm.Company.Application.ViewModel.Atendimentos;

public class AtendimentoViewModel : BaseViewModel
{
    public StatusAtendimento Status { get; set; }
    public Guid? UsuarioId { get; set; }
    public UsuarioViewModel? Usuario { get; set; }
    public Guid? UsuarioCancelamentoId { get; set; }
    public UsuarioViewModel? UsuarioCancelamento { get; set; }
    public Guid? UsuarioFinalizadoId { get; set; }
    public UsuarioViewModel? UsuarioFinalizado { get; set; }
    public string? Observacao { get; private set; }
    public string? MotivoCancelamento { get; set; }
    public List<MensagemAtendimentoViewModel> Mensagens { get; set; } = [];
    public ClienteViewModel Cliente { get; set; } = null!;
    public int MensagensNaoLidas { get; set; }

    public static explicit operator AtendimentoViewModel(Atendimento atendimento)
    {
        return new AtendimentoViewModel()
        {
            AtualizadoEm = atendimento.AtualizadoEm,
            CriadoEm = atendimento.CriadoEm,
            Id = atendimento.Id,
            MotivoCancelamento = atendimento.MotivoCancelamento,
            Numero = atendimento.Numero,
            Observacao = atendimento.Observacao,
            Status = atendimento.Status,
            Usuario = atendimento.Usuario != null ? (UsuarioViewModel)atendimento.Usuario : null,
            UsuarioCancelamento = atendimento.UsuarioCancelamento != null ? (UsuarioViewModel)atendimento.UsuarioCancelamento : null,
            UsuarioCancelamentoId = atendimento.UsuarioCancelamentoId,
            UsuarioFinalizado = atendimento.UsuarioFinalizado != null ? (UsuarioViewModel)atendimento.UsuarioFinalizado : null,
            UsuarioFinalizadoId = atendimento.UsuarioFinalizadoId,
            UsuarioId = atendimento.UsuarioId,
            Mensagens = atendimento.Mensagens.Select(x => (MensagemAtendimentoViewModel)x).ToList(),
            Cliente = atendimento.Cliente != null ? (ClienteViewModel)atendimento.Cliente : null!
        };
    }
}
