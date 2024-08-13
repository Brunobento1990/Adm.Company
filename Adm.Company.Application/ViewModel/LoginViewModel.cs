namespace Adm.Company.Application.ViewModel;

public class LoginViewModel
{
    public string Token { get; set; } = string.Empty;
    public UsuarioViewModel Usuario { get; set; } = null!;
    public ConfiguracaoAtendimentoEmpresaViewModel? ConfiguracaoAtendimentoEmpresa { get; set; }
}
