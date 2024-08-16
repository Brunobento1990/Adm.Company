using Adm.Company.Infrastructure.HttpServices.Interfaces;
using Adm.Company.Infrastructure.HttpServices.Responses.WhatsApi;
using System.Text.Json;
using System.Text;
using Adm.Company.Infrastructure.HttpServices.Responses;

namespace Adm.Company.Infrastructure.HttpServices.Classes;

public sealed class ChatWhatsHttpService : IChatWhatsHttpService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private const string URL_CONNECT_INSTANCE = "/chat/";

    public ChatWhatsHttpService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        
    }
    public async Task<IList<ContatoResponse>> GetContatosAsync(string instanceName)
    {
        var client = _httpClientFactory.CreateClient("WHATS");
        var response = await client.PostAsync($"{URL_CONNECT_INSTANCE}findContacts/{instanceName}", new StringContent(
                JsonSerializer.Serialize(new { }),
                Encoding.UTF8,
                "application/json"));
        var body = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Erro evolution api: {body}");
            return [];
        }
        return JsonSerializer.Deserialize<IList<ContatoResponse>>(body, JsonOptionsModel.Options) ?? [];
    }

    public async Task<FetchInstanceResponse?> GetPerfilAsync(string instanceName)
    {
        var client = _httpClientFactory.CreateClient("WHATS");
        var response = await client.GetAsync($"instance/fetchInstances?instanceName={instanceName}");
        var body = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Erro evolution api: {body}");
            return null;
        }
        return JsonSerializer.Deserialize<FetchInstanceResponse>(body, JsonOptionsModel.Options);
    }
}
