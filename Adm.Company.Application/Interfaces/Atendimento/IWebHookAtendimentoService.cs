namespace Adm.Company.Application.Interfaces.Atendimento;

public interface IWebHookAtendimentoService
{
    Task CreateOrUpdateAtendimentoWebHookAsync(
        string mensagem, 
        string numeroWhatsEmpresa, 
        string numeroWhatsOrigem, 
        string remoteId, 
        string tipoMensagem, 
        string nome,
        bool fromMe,
        string? caption);
}
