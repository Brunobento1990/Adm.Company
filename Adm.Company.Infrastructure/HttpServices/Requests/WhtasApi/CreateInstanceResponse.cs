using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text;

namespace Adm.Company.Infrastructure.HttpServices.Requests.WhtasApi;

public class CreateInstanceResponse
{
    public string InstanceName { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public bool Qrcode { get; set; }

    public StringContent ToJson()
    {
        var json = JsonSerializer.Serialize(this, new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
        });

        return new StringContent(
                json,
                Encoding.UTF8,
                "application/json");
    }
}
