namespace Adm.Company.Infrastructure.HttpServices.Requests.WhtasApi;

public class UpdateContactRequest
{
    public string Instance { get; set; } = string.Empty;
    public IList<DataUpdateContactRequest> Data { get; set; } = [];
}

public class DataUpdateContactRequest
{
    public string? RemoteJid { get; set; }
    public string? PushName { get; set; }
    public string? ProfilePicUrl { get; set; }
}
