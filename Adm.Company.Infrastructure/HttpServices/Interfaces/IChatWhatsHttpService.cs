using Adm.Company.Infrastructure.HttpServices.Requests.WhtasApi;
using Adm.Company.Infrastructure.HttpServices.Responses.WhatsApi;

namespace Adm.Company.Infrastructure.HttpServices.Interfaces;

public interface IChatWhatsHttpService
{
    Task<IList<ContatoResponse>> GetContatosAsync(string instanceName);
    Task<IList<FetchInstanceResponse>?> GetPerfilAsync(string instanceName);
    Task<PerfilClienteWhatsResponse?> GetPerfilClienteAsync(string instanceName, string remoteJid);
    Task<EnviarMensagemResponse?> EnviarMensagemAsync(string instanceName, EnviarMensagemRequest enviarMensagemRequest);
    Task<ConvertAudioResponse?> ConvertAudioMensagemAsync(string instanceName, ConvertAudioRequest convertAudioRequest);
    Task<EnviarMensagemResponse?> EnviarAudioAsync(string instanceName, EnviarAudioRequest enviarAudioRequest);
}
