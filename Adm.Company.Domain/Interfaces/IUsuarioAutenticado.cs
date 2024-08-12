namespace Adm.Company.Domain.Interfaces;

public interface IUsuarioAutenticado
{
    Guid Id { get; set; }
    string Cpf { get; set; }
}
