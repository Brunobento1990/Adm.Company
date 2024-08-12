namespace Adm.Company.Api.Configurations;

public static class ConfigurationsCors
{
    public static IServiceCollection InjectCors(this IServiceCollection services, string origin)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(name: "base",
                              policy =>
                              {
                                  policy.WithOrigins(origin)
                                      .AllowAnyMethod()
                                      .AllowAnyHeader()
                                      .AllowCredentials();
                              });
        });

        return services;
    }
}
