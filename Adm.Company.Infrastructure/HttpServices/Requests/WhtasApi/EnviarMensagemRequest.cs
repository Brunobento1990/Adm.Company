using Adm.Company.Infrastructure.HttpServices.Responses;
using System.Text.Json;
using System.Text;

namespace Adm.Company.Infrastructure.HttpServices.Requests.WhtasApi;

public class EnviarMensagemRequest
{
    public string Number { get; set; } = string.Empty;
    public OptionEnviarMensagem Options { get; set; } = null!;
    public TextMensagem TextMessage { get; set; } = null!;

    public StringContent ToJson()
    {
        var json = JsonSerializer.Serialize(this, JsonOptionsModel.Options);

        return new StringContent(
                json,
                Encoding.UTF8,
                "application/json");
    }
}

public class OptionEnviarMensagem
{
    public int Delay { get; set; } = 1200;
    public string Presence { get; set; } = "composing";
    public bool LinkPreview { get; set; } = false;
}

public class TextMensagem
{
    public string Text { get; set; } = string.Empty;
}
