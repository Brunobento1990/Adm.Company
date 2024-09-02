using Adm.Company.Infrastructure.HttpServices.Responses;
using System.Text.Json;
using System.Text;

namespace Adm.Company.Infrastructure.HttpServices.Requests.WhtasApi;

public class EnviarAudioRequest
{
    public string Number { get; set; } = string.Empty;
    public string Audio { get; set; } = string.Empty;
    public Quoted? Quoted { get; set; }

    public StringContent ToJson()
    {
        var json = JsonSerializer.Serialize(this, JsonOptionsModel.Options);

        return new StringContent(
                json,
                Encoding.UTF8,
                "application/json");
    }
}

public class Quoted
{
    public QuotedKey? Key { get; set; }
    public QuotedMessage? QuotedMessage { get; set; }
}

public class QuotedKey
{
    public string RemoteJid { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
    public bool FromMe { get; set; }
}

public class QuotedMessage
{
    public string Conversation { get; set; } = string.Empty;
}
