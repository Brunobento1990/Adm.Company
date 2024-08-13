using Adm.Company.Application.ViewModel.Jwt;
using Adm.Company.Domain.Interfaces;
using Adm.Company.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Adm.Company.IoC.Context;

public static class ContextDependencyInject
{
    public static IServiceCollection InjectContext(this IServiceCollection services, string connectionString)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        services.AddDbContext<AdmCompanyContext>(opt => opt.UseNpgsql(connectionString));
        services.AddScoped<IUsuarioAutenticado, UsuarioAutenticado>();
        services.AddScoped<IEmpresaAutenticada, EmpresaAutenticada>();

        return services;
    }
}
