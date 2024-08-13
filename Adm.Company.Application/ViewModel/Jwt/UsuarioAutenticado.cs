using Adm.Company.Domain.Interfaces;

namespace Adm.Company.Application.ViewModel.Jwt;

public sealed class UsuarioAutenticado : IUsuarioAutenticado
{
    public Guid Id { get; set; }
    public Guid EmpresaId { get; set; }
    public string Cpf { get; set; } = string.Empty;
}
