using Adm.Company.Infrastructure.HttpServices.Responses;
using System.Text.Json;
using System.Text;

namespace Adm.Company.Infrastructure.HttpServices.Requests.WhtasApi;

public class ConvertAudioRequest
{
    public bool ConvertToMp4 { get; set; } = false;
    public MessageConvertAudioRequest Message { get; set; } = null!;

    public StringContent ToJson()
    {
        var json = JsonSerializer.Serialize(this, JsonOptionsModel.Options);

        return new StringContent(
                json,
                Encoding.UTF8,
                "application/json");
    }
}

public class MessageConvertAudioRequest
{
    public KeyConvertAudioRequest Key { get; set; } = null!;
}

public class KeyConvertAudioRequest
{
    public string Id { get; set; } = string.Empty;
}
