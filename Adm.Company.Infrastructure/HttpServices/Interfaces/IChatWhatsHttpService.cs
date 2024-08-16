using Adm.Company.Infrastructure.HttpServices.Responses.WhatsApi;

namespace Adm.Company.Infrastructure.HttpServices.Interfaces;

public interface IChatWhatsHttpService
{
    Task<IList<ContatoResponse>> GetContatosAsync(string instanceName);
    Task<FetchInstanceResponse?> GetPerfilAsync(string instanceName);
}
