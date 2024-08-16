using System.Text.Json;
using System.Text.Json.Serialization;

namespace Adm.Company.Infrastructure.HttpServices.Responses;

public static class JsonOptionsModel
{
    public static JsonSerializerOptions Options { get; } = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
}
