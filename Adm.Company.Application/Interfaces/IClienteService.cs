using Adm.Company.Application.Dtos.Clientes;
using Adm.Company.Application.ViewModel;
using Adm.Company.Infrastructure.HttpServices.Requests.WhtasApi;

namespace Adm.Company.Application.Interfaces;

public interface IClienteService
{
    Task AddClientesFromWhatsAsync(UpdateContactRequest updateContactRequest);
    Task<IList<ClienteViewModel>> PaginacaoAsync(PaginacaoClienteWhatsDto paginacaoClienteWhatsDto);
}
