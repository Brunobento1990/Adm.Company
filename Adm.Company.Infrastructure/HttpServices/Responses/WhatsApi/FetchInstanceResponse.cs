namespace Adm.Company.Infrastructure.HttpServices.Responses.WhatsApi;

public class FetchInstanceResponse
{
    public InstanceBody Instance { get; set; } = null!;
}

public class InstanceBody
{
    public string ProfileName { get; set; } = string.Empty;
    public string ProfilePictureUrl { get; set; } = string.Empty;
}
