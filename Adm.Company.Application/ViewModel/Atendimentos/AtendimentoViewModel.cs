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
    public string? Observacao { get; private set; }
    public string? MensagemCancelamento { get; set; }
    public List<MensagemAtendimentoViewModel> Mensagens { get; set; } = [];
    public ClienteViewModel Cliente { get; set; } = null!;

    public static explicit operator AtendimentoViewModel(Atendimento atendimento)
    {
        return new AtendimentoViewModel()
        {
            AtualizadoEm = atendimento.AtualizadoEm,
            CriadoEm = atendimento.CriadoEm,
            Id = atendimento.Id,
            MensagemCancelamento = atendimento.MensagemCancelamento,
            Numero = atendimento.Numero,
            Observacao = atendimento.Observacao,
            Status = atendimento.Status,
            Usuario = atendimento.Usuario != null ? (UsuarioViewModel)atendimento.Usuario : null,
            UsuarioCancelamento = atendimento.UsuarioCancelamento != null ? (UsuarioViewModel)atendimento.UsuarioCancelamento : null,
            UsuarioCancelamentoId = atendimento.UsuarioCancelamentoId,
            UsuarioId = atendimento.UsuarioId,
            Mensagens = atendimento.Mensagens.Select(x => (MensagemAtendimentoViewModel)x).ToList(),
            Cliente = atendimento.Cliente != null ? (ClienteViewModel)atendimento.Cliente : null!
        };
    }
}
