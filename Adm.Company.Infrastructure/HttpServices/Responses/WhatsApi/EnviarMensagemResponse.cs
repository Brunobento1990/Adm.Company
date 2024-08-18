namespace Adm.Company.Infrastructure.HttpServices.Responses.WhatsApi;

public class EnviarMensagemResponse
{
    public KeyEnviarMensagemResponse? Key { get; set; }
}

public class KeyEnviarMensagemResponse
{
    public string Id { get; set; } = string.Empty;
}
