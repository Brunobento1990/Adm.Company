namespace Adm.Company.Domain.Interfaces;

public interface IUsuarioAutenticado
{
    Guid Id { get; set; }
    Guid EmpresaId { get; set; }
    string Cpf { get; set; }
}
