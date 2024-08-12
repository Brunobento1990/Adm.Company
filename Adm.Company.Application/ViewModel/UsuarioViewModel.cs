using Adm.Company.Domain.Entities;

namespace Adm.Company.Application.ViewModel;

public class UsuarioViewModel : BaseViewModel
{
    public string Cpf { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? WhatsApp { get; set; } = string.Empty;
    public EmpresaViewModel Empresa { get; set; } = null!;

    public static explicit operator UsuarioViewModel(Usuario usuario)
    {
        return new UsuarioViewModel()
        {
            AtualizadoEm = usuario.AtualizadoEm,
            Cpf = usuario.Cpf,
            Email = usuario.Email,
            CriadoEm = usuario.CriadoEm,
            Empresa = usuario.Empresa != null ? (EmpresaViewModel)usuario.Empresa : null!,
            Id = usuario.Id,
            Numero = usuario.Numero,
            WhatsApp = usuario.WhatsApp
        };
    }
}
