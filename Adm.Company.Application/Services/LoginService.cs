using Adm.Company.Application.Adapters;
using Adm.Company.Application.Dtos.Login;
using Adm.Company.Application.Interfaces;
using Adm.Company.Application.ViewModel;
using Adm.Company.Domain.Exceptions;
using Adm.Company.Domain.Interfaces;

namespace Adm.Company.Application.Services;

public sealed class LoginService : ILoginService
{
    private readonly ILoginUsuarioRepository _loginUsuarioRepository;
    private readonly ITokenService _tokenService;
    private readonly IConfiguracaoAtendimentoEmpresaRepository _configuracaoAtendimentoEmpresaRepository;

    public LoginService(ILoginUsuarioRepository loginUsuarioRepository, ITokenService tokenService, IConfiguracaoAtendimentoEmpresaRepository configuracaoAtendimentoEmpresaRepository)
    {
        _loginUsuarioRepository = loginUsuarioRepository;
        _tokenService = tokenService;
        _configuracaoAtendimentoEmpresaRepository = configuracaoAtendimentoEmpresaRepository;
    }

    public async Task<LoginViewModel> LoginAsync(LoginUsuarioDto loginUsuarioDto)
    {
        var usuario = await _loginUsuarioRepository.LoginAsync(loginUsuarioDto.Email);

        if (usuario == null || !PasswordAdapter.VerifyPassword(loginUsuarioDto.Senha, usuario.Senha))
            throw new ExceptionApiUnauthorized("E-mail ou senha inválidos!");

        if (usuario.Bloqueado)
        {
            throw new ExceptionApiUnauthorized("Usuário bloqueado!");
        }

        var configuracaoAtendimento = await _configuracaoAtendimentoEmpresaRepository
            .GetConfiguracaoAtendimentoEmpresaByEmpresaIdAsync(usuario.EmpresaId);

        return new LoginViewModel()
        {
            Token = _tokenService.GetToken(usuario),
            Usuario = (UsuarioViewModel)usuario,
            ConfiguracaoAtendimentoEmpresa = configuracaoAtendimento != null ? (ConfiguracaoAtendimentoEmpresaViewModel)configuracaoAtendimento : null
        };
    }
}
