using Adm.Company.Infrastructure.HttpServices.Interfaces;
using Adm.Company.Infrastructure.HttpServices.Requests.WhtasApi;
using Adm.Company.Infrastructure.HttpServices.Responses;
using Adm.Company.Infrastructure.HttpServices.Responses.WhatsApi;
using System.Text.Json;

namespace Adm.Company.Infrastructure.HttpServices.Classes;

public sealed class WhatsHttpService : IWhatsHttpService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private const string URL_CONNECT_INSTANCE = "/instance/";

    public WhatsHttpService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<ConnectInstanceResponse?> ConnectInstanceAsync(string instanceName)
    {
        var client = _httpClientFactory.CreateClient("WHATS");
        var response = await client.GetAsync($"{URL_CONNECT_INSTANCE}connectionState/{instanceName}");
        var body = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Erro evolution api: {body}");
            return null;
        }
        return JsonSerializer.Deserialize<ConnectInstanceResponse>(body, JsonOptionsModel.Options);
    }

    public async Task<InstanceCreateResponse?> CreateInstanceAsync(CreateInstanceResponse createInstanceResponse)
    {
        var client = _httpClientFactory.CreateClient("WHATS");
        var response = await client.PostAsync($"{URL_CONNECT_INSTANCE}create", createInstanceResponse.ToJson());
        var body = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Erro evolution api: {body}");
            return null;
        }
        return JsonSerializer.Deserialize<InstanceCreateResponse>(body, JsonOptionsModel.Options);
    }

    public async Task<InstanceConnectingResponse?> GetConnectInstanceAsync(string instanceName)
    {
        var client = _httpClientFactory.CreateClient("WHATS");
        var response = await client.GetAsync($"{URL_CONNECT_INSTANCE}connect/{instanceName}");
        var body = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Erro evolution api: {body}");
            return null;
        }
        return JsonSerializer.Deserialize<InstanceConnectingResponse>(body, JsonOptionsModel.Options);
    }

    public async Task<bool> LogoutInstanceAsync(string instanceName)
    {
        var client = _httpClientFactory.CreateClient("WHATS");
        var response = await client.DeleteAsync($"{URL_CONNECT_INSTANCE}logout/{instanceName}");
        var body = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Erro evolution api: {body}");
            return false;
        }

        var result = JsonSerializer.Deserialize<LogoutInstanceResponse>(body, JsonOptionsModel.Options);

        if (result == null)
        {
            return false;
        }

        return result.Error;
    }

    public async Task<bool> DeleteInstanceAsync(string instanceName)
    {
        var client = _httpClientFactory.CreateClient("WHATS");
        var response = await client.DeleteAsync($"{URL_CONNECT_INSTANCE}delete/{instanceName}");
        var body = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Erro evolution api: {body}");
            return false;
        }

        var result = JsonSerializer.Deserialize<LogoutInstanceResponse>(body, JsonOptionsModel.Options);

        if (result == null)
        {
            return false;
        }

        return result.Error;
    }
}
