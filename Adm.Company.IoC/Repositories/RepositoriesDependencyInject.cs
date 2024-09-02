using Adm.Company.Domain.Interfaces;
using Adm.Company.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Adm.Company.IoC.Repositories;

public static class RepositoriesDependencyInject
{
    public static IServiceCollection InjectRepositories(this IServiceCollection services)
    {
        services.AddScoped<ILoginUsuarioRepository, LoginUsuarioRepository>();
        services.AddScoped<IEmpresaRepository, EmpresaRepository>();
        services.AddScoped<IConfiguracaoAtendimentoEmpresaRepository, ConfiguracaoAtendimentoEmpresaRepository>();
        services.AddScoped<IAtendimentoRepository, AtendimentoRepository>();
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IMensagemAtendimentoRepository, MensagemAtendimentoRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();

        return services;
    }
}
