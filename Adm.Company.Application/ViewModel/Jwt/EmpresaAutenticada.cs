using Adm.Company.Domain.Interfaces;

namespace Adm.Company.Application.ViewModel.Jwt;

public class EmpresaAutenticada : IEmpresaAutenticada
{
    public Guid Id { get ; set ; }
}
