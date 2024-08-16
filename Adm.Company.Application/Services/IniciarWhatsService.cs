using Adm.Company.Application.Interfaces;
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

    public IniciarWhatsService(
        IChatWhatsHttpService chatWhatsHttpService,
        IConfiguracaoAtendimentoEmpresaRepository configuracaoAtendimentoEmpresaRepository,
        IEmpresaAutenticada empresaAutenticada)
    {
        _chatWhatsHttpService = chatWhatsHttpService;
        _configuracaoAtendimentoEmpresaRepository = configuracaoAtendimentoEmpresaRepository;
        _empresaAutenticada = empresaAutenticada;
    }

    public async Task<IniciarWhatsViewModel> GetPerfilAsync()
    {
        var configuracaoWhats = await GetConfiguracaoAtendimentoEmpresaAsync();
        var perfil = await _chatWhatsHttpService.GetPerfilAsync(configuracaoWhats.WhatsApp)
            ?? throw new ExceptionApiErro("Não foi possível obter seu perfil do whtas!");
        var contatos = await _chatWhatsHttpService.GetContatosAsync(configuracaoWhats.WhatsApp);

        var perfilViewModel = new PerfilWhatsViewModel()
        {
            Foto = perfil.Instance.ProfilePictureUrl,
            Nome = perfil.Instance.ProfileName
        };

        var contatosViewModel = contatos.Select(x => new ContatoWhatsViewModel()
        {
            Foto = x.ProfilePictureUrl,
            Id = x.Id,
            Nome = x.PushName,
            Numero = x.Owner
        }).ToList();

        return new IniciarWhatsViewModel()
        {
            Contatos = contatosViewModel,
            Perfil = perfilViewModel
        };
    }

    async Task<ConfiguracaoAtendimentoEmpresa> GetConfiguracaoAtendimentoEmpresaAsync()
    {
        return await _configuracaoAtendimentoEmpresaRepository.GetConfiguracaoAtendimentoEmpresaByEmpresaIdAsync(_empresaAutenticada.Id)
            ?? throw new ExceptionApiErro("A configuração do whtas não foi localizada!");
    }
}
