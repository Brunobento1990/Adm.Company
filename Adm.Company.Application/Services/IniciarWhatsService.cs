using Adm.Company.Application.Interfaces;
using Adm.Company.Application.Interfaces.Atendimentos;
using Adm.Company.Application.ViewModel.WhatsApi;
using Adm.Company.Domain.Entities;
using Adm.Company.Domain.Exceptions;
using Adm.Company.Domain.Interfaces;
using Adm.Company.Infrastructure.HttpServices.Interfaces;

namespace Adm.Company.Application.Services;

public sealed class IniciarWhatsService : IIniciarWhatsService
{
    private readonly IChatWhatsHttpService _chatWhatsHttpService;
    private readonly IConfiguracaoAtendimentoEmpresaRepository _configuracaoAtendimentoEmpresaRepository;
    private readonly IEmpresaAutenticada _empresaAutenticada;
    private readonly IAtendimentoService _atendimentoService;

    public IniciarWhatsService(
        IChatWhatsHttpService chatWhatsHttpService,
        IConfiguracaoAtendimentoEmpresaRepository configuracaoAtendimentoEmpresaRepository,
        IEmpresaAutenticada empresaAutenticada,
        IAtendimentoService atendimentoService)
    {
        _chatWhatsHttpService = chatWhatsHttpService;
        _configuracaoAtendimentoEmpresaRepository = configuracaoAtendimentoEmpresaRepository;
        _empresaAutenticada = empresaAutenticada;
        _atendimentoService = atendimentoService;
    }

    public async Task<IniciarWhatsViewModel> GetPerfilAsync()
    {
        var configuracaoWhats = await GetConfiguracaoAtendimentoEmpresaAsync();
        var perfil = await _chatWhatsHttpService.GetPerfilAsync(configuracaoWhats.WhatsApp);

        var perfilViewModel = new PerfilWhatsViewModel()
        {
            Foto = perfil?.FirstOrDefault()?.ProfilePicUrl ?? string.Empty,
            Nome = perfil?.FirstOrDefault()?.ProfileName ?? string.Empty
        };

        var atendimentos = await _atendimentoService.MeusAtendimentosAsync();

        return new IniciarWhatsViewModel()
        {
            Perfil = perfilViewModel,
            Atendimentos = atendimentos
        };
    }

    async Task<ConfiguracaoAtendimentoEmpresa> GetConfiguracaoAtendimentoEmpresaAsync()
    {
        return await _configuracaoAtendimentoEmpresaRepository.GetConfiguracaoAtendimentoEmpresaByEmpresaIdAsync(_empresaAutenticada.Id)
            ?? throw new ExceptionApiErro("A configuração do whtas não foi localizada!");
    }
}
