using Adm.Company.Application.ViewModel;

namespace Adm.Company.Application.Interfaces;

public interface IUsuarioService
{
    Task<IList<UsuarioViewModel>> GetPaginacaoAsync(int skip, string? search);
}
