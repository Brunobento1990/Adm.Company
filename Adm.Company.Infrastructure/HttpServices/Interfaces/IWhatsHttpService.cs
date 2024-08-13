using Adm.Company.Infrastructure.HttpServices.Requests.WhtasApi;
using Adm.Company.Infrastructure.HttpServices.Responses.WhatsApi;

namespace Adm.Company.Infrastructure.HttpServices.Interfaces;

public interface IWhatsHttpService
{
    Task<ConnectInstanceResponse?> ConnectInstanceAsync(string instanceName);
    Task<InstanceConnectingResponse?> GetConnectInstanceAsync(string instanceName);
    Task<FetchInstanceResponse?> FetchInstanceAsync(string instanceName);
    Task<IList<ContatoResponse>> GetContatosAsync(string instanceName);
    Task<InstanceCreateResponse?> CreateInstanceAsync(CreateInstanceResponse createInstanceResponse);
}
