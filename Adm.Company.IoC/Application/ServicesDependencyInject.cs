using Adm.Company.Application.Interfaces;
using Adm.Company.Application.Interfaces.Atendimento;
using Adm.Company.Application.Services;
using Adm.Company.Application.Services.Atendimentos;
using Microsoft.Extensions.DependencyInjection;

namespace Adm.Company.IoC.Application;

public static class ServicesDependencyInject
{
    public static IServiceCollection InjectServices(this IServiceCollection services)
    {
        services.AddScoped<IWhatsServiceInstanceService, WhatsServiceInstanceService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<IIniciarWhatsService, IniciarWhatsService>();
        services.AddScoped<IWebHookAtendimentoService, WebHookAtendimentoService>();
        services.AddScoped<IAtendimentoService, AtendimentoService>();
        services.AddScoped<IEnviarMensagemAtendimentoService, EnviarMensagemAtendimentoService>();
        services.AddScoped<IAtualizarMensagemAtendimentoService, AtualizarMensagemAtendimentoService>();
        services.AddScoped<IMensagemAtendimentoService, MensagemAtendimentoService>();
        services.AddScoped<IClienteService, ClienteService>();

        return services;
    }
}
