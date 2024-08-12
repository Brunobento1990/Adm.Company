namespace Adm.Company.Infrastructure.HttpServices.Responses.WhatsApi;

public class ConnectInstanceResponse
{
    public InstanceStatusConnectResponse Instance { get; set; } = null!;
}

public class InstanceStatusConnectResponse
{
    public string InstanceName { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
}

public class InstanceConnectingResponse
{
    public string Base64 { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
}
