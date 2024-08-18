namespace Adm.Company.Application.Interfaces.Atendimento;

public interface IAtualizarMensagemAtendimentoService
{
    Task AtualizarAsync(string instance, string status, string remoteId);
}
