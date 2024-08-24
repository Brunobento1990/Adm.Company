using Adm.Company.Infrastructure.HttpServices.Interfaces;
using Adm.Company.Infrastructure.HttpServices.Responses.WhatsApi;
using System.Text.Json;
using System.Text;
using Adm.Company.Infrastructure.HttpServices.Responses;
using Adm.Company.Infrastructure.HttpServices.Requests.WhtasApi;

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

    public async Task<IList<FetchInstanceResponse>?> GetPerfilAsync(string instanceName)
    {
        var client = _httpClientFactory.CreateClient("WHATS");
        var response = await client.GetAsync($"instance/fetchInstances?instanceName={instanceName}");
        var body = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Erro evolution api: {body}");
            return null;
        }
        return JsonSerializer.Deserialize<IList<FetchInstanceResponse>?>(body, JsonOptionsModel.Options);
    }

    public async Task<PerfilClienteWhatsResponse?> GetPerfilClienteAsync(string instanceName, string remoteJid)
    {
        var client = _httpClientFactory.CreateClient("WHATS");
        var response = await client.PostAsync($"{URL_CONNECT_INSTANCE}fetchProfilePictureUrl/{instanceName}", new StringContent(
                JsonSerializer.Serialize(new { number = remoteJid }),
                Encoding.UTF8,
                "application/json"));
        var body = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Erro evolution api: {body}");
            return null;
        }
        return JsonSerializer.Deserialize<PerfilClienteWhatsResponse>(body, JsonOptionsModel.Options);
    }

    public async Task<EnviarMensagemResponse?> EnviarMensagemAsync(string instanceName, EnviarMensagemRequest enviarMensagemRequest)
    {
        var client = _httpClientFactory.CreateClient("WHATS");
        var response = await client.PostAsync($"message/sendText/{instanceName}", enviarMensagemRequest.ToJson());
        var body = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Erro evolution api: {body}");
            return null;
        }
        return JsonSerializer.Deserialize<EnviarMensagemResponse>(body, JsonOptionsModel.Options);
    }

    public async Task<ConvertAudioResponse?> ConvertAudioMensagemAsync(string instanceName, ConvertAudioRequest convertAudioRequest)
    {
        var client = _httpClientFactory.CreateClient("WHATS");
        var response = await client
            .PostAsync($"{URL_CONNECT_INSTANCE}getBase64FromMediaMessage/{instanceName}", convertAudioRequest.ToJson());

        var body = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine(body);
            return null;
        }

        return JsonSerializer.Deserialize<ConvertAudioResponse>(body, JsonOptionsModel.Options);
    }

    public async Task<EnviarMensagemResponse?> EnviarAudioAsync(string instanceName, EnviarAudioRequest enviarAudioRequest)
    {
        var client = _httpClientFactory.CreateClient("WHATS");
        var response = await client.PostAsync($"/message/sendWhatsAppAudio/{instanceName}", enviarAudioRequest.ToJson());
            var body = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine(body);
            return null;
        }
        return JsonSerializer.Deserialize<EnviarMensagemResponse>(body, JsonOptionsModel.Options);
    }
}
