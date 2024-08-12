using Adm.Company.Application.Interfaces;
using Adm.Company.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Adm.Company.IoC.Application;

public static class ServicesDependencyInject
{
    public static IServiceCollection InjectServices(this IServiceCollection services)
    {
        services.AddScoped<IWhatsServiceInstanceService, WhatsServiceInstanceService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ILoginService, LoginService>();

        return services;
    }
}
