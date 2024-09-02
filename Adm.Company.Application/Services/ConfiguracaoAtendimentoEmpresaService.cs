using Adm.Company.Application.Dtos.ConfiguracoesAtendimentoEmpresa;
using Adm.Company.Application.Interfaces;
using Adm.Company.Application.ViewModel;
using Adm.Company.Domain.Entities;
using Adm.Company.Domain.Exceptions;
using Adm.Company.Domain.Interfaces;
using Adm.Company.Infrastructure.HttpServices.Interfaces;

namespace Adm.Company.Application.Services;

public sealed class ConfiguracaoAtendimentoEmpresaService : IConfiguracaoAtendimentoEmpresaService
{
    private readonly IConfiguracaoAtendimentoEmpresaRepository _configuracaoAtendimentoEmpresaRepository;
    private readonly IUsuarioAutenticado _usuarioAutenticado;
    private readonly IWhatsHttpService _whatsHttpService;
    private readonly IUsuarioRepository _usuarioRepository;
    public ConfiguracaoAtendimentoEmpresaService(
        IConfiguracaoAtendimentoEmpresaRepository configuracaoAtendimentoEmpresaRepository,
        IUsuarioAutenticado usuarioAutenticado,
        IWhatsHttpService whatsHttpService,
        IUsuarioRepository usuarioRepository)
    {
        _configuracaoAtendimentoEmpresaRepository = configuracaoAtendimentoEmpresaRepository;
        _usuarioAutenticado = usuarioAutenticado;
        _whatsHttpService = whatsHttpService;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<ConfiguracaoAtendimentoEmpresaViewModel> CreateOrUpdateAsync(
        ConfiguracaoAtendimentoEmpresaDto configuracaoAtendimentoEmpresaDto)
    {
        configuracaoAtendimentoEmpresaDto.Validar();

        var configuracao = await _configuracaoAtendimentoEmpresaRepository
            .GetConfiguracaoAtendimentoEmpresaByEmpresaIdAsync(empresaId: _usuarioAutenticado.EmpresaId);

        var configuracaoWhats = await _configuracaoAtendimentoEmpresaRepository
            .GetConfiguracaoAtendimentoEmpresaByNumeroWhtasAsync(numeroWhats: configuracaoAtendimentoEmpresaDto.WhatsApp);

        if (configuracaoWhats != null && configuracaoWhats.EmpresaId != _usuarioAutenticado.EmpresaId)
        {
            throw new ExceptionApiErro($"Este número já se encontra cadastrado: {configuracaoAtendimentoEmpresaDto.WhatsApp}");
        }

        if (configuracaoAtendimentoEmpresaDto.UsuarioId.HasValue)
        {
            _ = await _usuarioRepository.GetByIdAsync(configuracaoAtendimentoEmpresaDto.UsuarioId.Value)
                ?? throw new ExceptionApiErro("O usuário para configuração de atendimento não foi lozalizado!");
        }

        if (configuracao == null)
        {
            configuracao = new ConfiguracaoAtendimentoEmpresa(
                id: Guid.NewGuid(),
                criadoEm: DateTime.Now,
                atualizadoEm: DateTime.Now,
                numero: 0,
                empresaId: _usuarioAutenticado.EmpresaId,
                whatsApp: configuracaoAtendimentoEmpresaDto.WhatsApp,
                primeiraMensagem: configuracaoAtendimentoEmpresaDto.PrimeiraMensagem,
                usuarioId: configuracaoAtendimentoEmpresaDto.UsuarioId);

            await _configuracaoAtendimentoEmpresaRepository.AddAsync(configuracao);

            return (ConfiguracaoAtendimentoEmpresaViewModel)configuracao;
        }

        if (configuracao.WhatsApp != configuracaoAtendimentoEmpresaDto.WhatsApp)
        {
            await _whatsHttpService.LogoutInstanceAsync(configuracao.WhatsApp);
            await _whatsHttpService.DeleteInstanceAsync(configuracao.WhatsApp);
        }

        configuracao.Update(
            whatsApp: configuracaoAtendimentoEmpresaDto.WhatsApp,
            primeiraMensagem: configuracaoAtendimentoEmpresaDto.PrimeiraMensagem,
            usuarioId: configuracaoAtendimentoEmpresaDto.UsuarioId);

        await _configuracaoAtendimentoEmpresaRepository.UpdateAsync(configuracao);

        return (ConfiguracaoAtendimentoEmpresaViewModel)configuracao;
    }

    public async Task<ConfiguracaoAtendimentoEmpresaViewModel> GetAsync()
    {
        var configuracao = await _configuracaoAtendimentoEmpresaRepository
            .GetConfiguracaoAtendimentoEmpresaByEmpresaIdAsync(empresaId: _usuarioAutenticado.EmpresaId);

        if (configuracao == null)
        {
            return new();
        }

        var configuracaoViewModel = (ConfiguracaoAtendimentoEmpresaViewModel)configuracao;

        if (configuracao.UsuarioId.HasValue)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(configuracao.UsuarioId.Value);
            if(usuario != null)
            {
                configuracaoViewModel.Usuario = (UsuarioViewModel)usuario;
            }
        }

        return configuracaoViewModel;
    }
}
