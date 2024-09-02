using Adm.Company.Application.Interfaces;
using Adm.Company.Application.ViewModel;
using Adm.Company.Domain.Interfaces;

namespace Adm.Company.Application.Services;

public sealed class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IUsuarioAutenticado _usuarioAutenticado;

    public UsuarioService(IUsuarioRepository usuarioRepository, IUsuarioAutenticado usuarioAutenticado)
    {
        _usuarioRepository = usuarioRepository;
        _usuarioAutenticado = usuarioAutenticado;
    }

    public async Task<IList<UsuarioViewModel>> GetPaginacaoAsync(int skip, string? search)
    {
        var usuarios = await _usuarioRepository.GetPaginacaoAsync(
            empresaId: _usuarioAutenticado.EmpresaId,
            skip: skip,
            search: search);

        return usuarios.Select(x => (UsuarioViewModel)x).ToList();
    }
}
