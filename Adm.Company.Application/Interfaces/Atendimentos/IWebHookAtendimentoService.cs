using Adm.Company.Infrastructure.HttpServices.Responses.WhatsApi;

namespace Adm.Company.Application.Interfaces.Atendimentos;

public interface IWebHookAtendimentoService
{
    Task CreateOrUpdateAtendimentoWebHookAsync(MensagemRecebidaWhatsResponse mensagemRecebidaWhatsResponse);
}
