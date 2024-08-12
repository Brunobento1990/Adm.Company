using Adm.Company.Domain.Entities;

namespace Adm.Company.Application.ViewModel;

public class EmpresaViewModel : BaseViewModel
{
    public string Cnpj { get; set; } = string.Empty;
    public string RazaoSocial { get; set; } = string.Empty;
    public string NomeFantasia { get; set; } = string.Empty;

    public static explicit operator EmpresaViewModel(Empresa empresa)
    {
        return new EmpresaViewModel()
        {
            AtualizadoEm = empresa.AtualizadoEm,
            Cnpj = empresa.Cnpj,    
            CriadoEm = empresa.CriadoEm,
            Id = empresa.Id,
            NomeFantasia = empresa.NomeFantasia,
            Numero = empresa.Numero,
            RazaoSocial = empresa.RazaoSocial
        };
    }
}
