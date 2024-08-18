using Adm.Company.Domain.Entities;

namespace Adm.Company.Application.ViewModel;

public class ClienteViewModel : BaseViewModel
{
    public string Nome { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? WhatsApp { get; set; }
    public string? Foto { get; set; }

    public static explicit operator ClienteViewModel(Cliente cliente)
    {
        return new ClienteViewModel()
        {
            AtualizadoEm = cliente.AtualizadoEm,
            Cpf = cliente.Cpf,
            Email = cliente.Email,
            CriadoEm = cliente.CriadoEm,
            Foto = cliente.Foto,
            Id = cliente.Id,
            Numero = cliente.Numero,
            WhatsApp = cliente.WhatsApp,
            Nome = cliente.Nome
        };
    }
}
