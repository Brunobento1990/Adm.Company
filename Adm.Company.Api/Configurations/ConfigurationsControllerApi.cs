using System.Text.Json.Serialization;

namespace Adm.Company.Api.Configurations;

public static class ConfigurationsControllerApi
{
    public static IServiceCollection ConfigureController(this IServiceCollection services)
    {
        services.AddControllers().AddJsonOptions(opt =>
        {
            opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });

        services.AddEndpointsApiExplorer();

        return services;
    }
}
