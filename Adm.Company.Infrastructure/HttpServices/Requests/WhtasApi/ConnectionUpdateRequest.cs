namespace Adm.Company.Infrastructure.HttpServices.Requests.WhtasApi;

public class DataConnectionUpdateRequest
{
    public string Instance { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public int StatusReason { get; set; }
}

public class ConnectionUpdateRequest
{
    public string Instance { get; set; } = string.Empty;
    public DataConnectionUpdateRequest Data { get; set; } = null!;
    public string Destination { get; set; } = string.Empty;
    public DateTime Date_time { get; set; }
    public string Server_url { get; set; } = string.Empty;
    public string Apikey { get; set; } = string.Empty;
}
