using Adm.Company.Api.Midllewares;

namespace Adm.Company.Api.Configurations;

public static class ConfigurationsMidllewares
{
    public static void AddMiddlewaresApi(this WebApplication app)
    {
        app.UseMiddleware<LogMiddleware>();
        app.UseMiddleware<AutenticaUsuarioMidlleware>();
        app.UseMiddleware<AutenticaEmpresaMidlleware>();
    }
}
