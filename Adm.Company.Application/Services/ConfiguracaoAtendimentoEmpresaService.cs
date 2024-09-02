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
        var configuracao = await _configuracaoAtendimentoEmpresaRepository
            .GetConfiguracaoAtendimentoEmpresaByEmpresaIdAsync(empresaId: _usuarioAutenticado.EmpresaId);

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
                whatsApp: configuracaoAtendimentoEmpresaDto.NumeroWhats,
                primeiraMensagem: configuracaoAtendimentoEmpresaDto.PrimeiraMensagem,
                usuarioId: configuracaoAtendimentoEmpresaDto.UsuarioId);

            await _configuracaoAtendimentoEmpresaRepository.AddAsync(configuracao);

            return (ConfiguracaoAtendimentoEmpresaViewModel)configuracao;
        }

        if (configuracao.WhatsApp != configuracaoAtendimentoEmpresaDto.NumeroWhats)
        {
            var resultLogout = await _whatsHttpService.LogoutInstanceAsync(configuracao.WhatsApp);

            if (!resultLogout)
            {
                throw new ExceptionApiErro("Não foi possível efetuar o logout do whats");
            }

            var resultDelete = await _whatsHttpService.DeleteInstanceAsync(configuracao.WhatsApp);

            if (!resultDelete)
            {
                throw new ExceptionApiErro("Não foi possível efetuar a exclusão do whats");
            }
        }

        configuracao.Update(
            whatsApp: configuracaoAtendimentoEmpresaDto.NumeroWhats,
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

        return (ConfiguracaoAtendimentoEmpresaViewModel)configuracao;
    }
}
