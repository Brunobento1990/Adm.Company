using Adm.Company.Domain.Entities;
using Adm.Company.Domain.Enums;

namespace Adm.Company.Application.ViewModel.Atendimentos;

public class MensagemAtendimentoViewModel : BaseViewModel
{
    public StatusMensagem Status { get; set; }
    public string Mensagem { get; set; } = string.Empty;
    public string TipoMensagem { get; set; } = string.Empty;
    public string RemoteId { get; set; } = string.Empty;
    public bool MinhaMensagem { get; set; }
    public Guid AtendimentoId { get; set; }

    public static explicit operator MensagemAtendimentoViewModel(MensagemAtendimento mensagemAtendimento)
    {
        return new MensagemAtendimentoViewModel()
        {
            AtendimentoId = mensagemAtendimento.AtendimentoId,
            AtualizadoEm = mensagemAtendimento.AtualizadoEm,
            CriadoEm = mensagemAtendimento.CriadoEm,
            Id = mensagemAtendimento.Id,
            Mensagem = mensagemAtendimento.Mensagem,
            MinhaMensagem = mensagemAtendimento.MinhaMensagem,
            Numero = mensagemAtendimento.Numero,
            RemoteId = mensagemAtendimento.RemoteId,
            Status = mensagemAtendimento.Status,
            TipoMensagem = mensagemAtendimento.TipoMensagem
        };
    }
}
