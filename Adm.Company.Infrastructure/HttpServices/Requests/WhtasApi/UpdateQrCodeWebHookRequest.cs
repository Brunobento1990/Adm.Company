namespace Adm.Company.Infrastructure.HttpServices.Requests.WhtasApi;

public class Data
{
    public Qrcode Qrcode { get; set; } = null!;
}

public class Qrcode
{
    public string Instance { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Base64 { get; set; } = string.Empty;
}

public class UpdateQrCodeWebHookRequest
{
    public string Instance { get; set; } = string.Empty;
    public Data Data { get; set; } = null!;
    public string Destination { get; set; } = string.Empty;
    public DateTime Date_time { get; set; }
    public string Server_url { get; set; } = string.Empty;
    public string Apikey { get; set; } = string.Empty;
}