using Adm.Company.Application.Dtos.Login;
using Adm.Company.Application.ViewModel;

namespace Adm.Company.Application.Interfaces;

public interface ILoginService
{
    Task<LoginViewModel> LoginAsync(LoginUsuarioDto loginUsuarioDto);
}
