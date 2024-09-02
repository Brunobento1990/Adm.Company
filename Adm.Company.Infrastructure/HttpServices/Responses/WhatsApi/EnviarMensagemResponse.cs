namespace Adm.Company.Infrastructure.HttpServices.Responses.WhatsApi;

public class EnviarMensagemResponse
{
    public KeyEnviarMensagemResponse? Key { get; set; }
}

public class KeyEnviarMensagemResponse
{
    public string Id { get; set; } = string.Empty;
}

public class ErroEnvioMensagemResponse
{
    public int Status { get; set; }
    public ErroResponseEnvioMensagemResponse Response { get; set; } = null!;
    public string Error { get; set; } = string.Empty;
}

public class ErroResponseEnvioMensagemResponse
{
    public string Message { get; set; } = string.Empty;
}
