using Adm.Company.Application.Hubs;

namespace Adm.Company.Api.Configurations;

public static class ConfigurationsHub
{
    public static void AddHubs(this WebApplication app)
    {
        app.MapHub<QrCodeUpdateWhatsHub>("/connection");
    }
}
