using Adm.Company.Infrastructure.HttpServices.Requests.WhtasApi;
using Adm.Company.Infrastructure.HttpServices.Responses.WhatsApi;

namespace Adm.Company.Infrastructure.HttpServices.Interfaces;

public interface IWhatsHttpService
{
    Task<ConnectInstanceResponse?> ConnectInstanceAsync(string instanceName);
    Task<InstanceConnectingResponse?> GetConnectInstanceAsync(string instanceName);
    Task<InstanceCreateResponse?> CreateInstanceAsync(CreateInstanceResponse createInstanceResponse);
    Task<bool> LogoutInstanceAsync(string instanceName);
    Task<bool> DeleteInstanceAsync(string instanceName);
}
