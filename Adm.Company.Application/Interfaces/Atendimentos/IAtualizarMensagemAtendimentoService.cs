namespace Adm.Company.Application.Interfaces.Atendimentos;

public interface IAtualizarMensagemAtendimentoService
{
    Task AtualizarAsync(string instance, string status, string remoteId);
}
