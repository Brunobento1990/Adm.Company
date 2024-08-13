using Adm.Company.Application.ViewModel.WhatsApi;

namespace Adm.Company.Application.Interfaces;

public interface IWhatsServiceInstanceService
{
    Task<ConnectInstanceViewModel?> ConnectInstanceAsync();
    Task<IniciarWhatsViewModel> GetPerfilAsync();
}
