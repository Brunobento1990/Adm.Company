using Adm.Company.Infrastructure.HttpServices.Requests.WhtasApi;

namespace Adm.Company.Application.Interfaces;

public interface IClienteService
{
    Task AddClientesFromWhatsAsync(UpdateContactRequest updateContactRequest);
}
