using Adm.Company.Application.ViewModel.WhatsApi;

namespace Adm.Company.Application.Interfaces;

public interface IIniciarWhatsService
{
    Task<IniciarWhatsViewModel> GetPerfilAsync();
}
