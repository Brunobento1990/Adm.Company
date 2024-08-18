using Adm.Company.Infrastructure.HttpServices.Requests.WhtasApi;
using Adm.Company.Infrastructure.HttpServices.Responses.WhatsApi;

namespace Adm.Company.Infrastructure.HttpServices.Interfaces;

public interface IChatWhatsHttpService
{
    Task<IList<ContatoResponse>> GetContatosAsync(string instanceName);
    Task<FetchInstanceResponse?> GetPerfilAsync(string instanceName);
    Task<PerfilClienteWhatsResponse?> GetPerfilClienteAsync(string instanceName, string remoteJid);
    Task<EnviarMensagemResponse?> EnviarMensagemAsync(string instanceName, EnviarMensagemRequest enviarMensagemRequest);
}
