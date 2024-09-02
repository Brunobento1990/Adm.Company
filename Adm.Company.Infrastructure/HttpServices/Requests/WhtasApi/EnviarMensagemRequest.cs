using Adm.Company.Infrastructure.HttpServices.Responses;
using System.Text.Json;
using System.Text;

namespace Adm.Company.Infrastructure.HttpServices.Requests.WhtasApi;

public class EnviarMensagemRequest
{
    public string Number { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
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
