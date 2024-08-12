using Adm.Company.Infrastructure.HttpServices.Classes;
using Adm.Company.Infrastructure.HttpServices.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Adm.Company.IoC.HttpClient;

public static class WhatsHttpClientDependencyInject
{
    public static IServiceCollection InjectWhatsHttp(this IServiceCollection services, string url, string apiKey)
    {
        services.AddScoped<IWhatsHttpService, WhatsHttpService>();
        services.AddHttpClient("WHATS", x =>
        {
            x.BaseAddress = new Uri(url);
            x.DefaultRequestHeaders.Add("apiKey", apiKey);
        });

        return services;
    }
}
