namespace Adm.Company.Infrastructure.HttpServices.Responses.WhatsApi;

public class MensagemRecebidaWhatsResponse
{
    public string Event { get; set; } = string.Empty;
    public string Sender { get; set; } = string.Empty;
    public string Instance { get; set; } = string.Empty;
    public DataMensagemRecebidaWhatsResponse Data { get; set; } = null!;
}

public class DataMensagemRecebidaWhatsResponse
{
    public string Owner { get; set; } = string.Empty;
    public string MessageType { get; set; } = string.Empty;
    public string PushName { get; set; } = string.Empty;
    public MensagemRecebida Message { get; set; } = null!;
}

public class MensagemRecebida
{
    public string Conversation { get; set; } = string.Empty;
}
