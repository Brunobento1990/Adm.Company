namespace Adm.Company.Infrastructure.HttpServices.Responses.WhatsApi;

public class Instance
{
    public string InstanceName { get; set; } = string.Empty;
    public string InstanceId { get; set; } = string.Empty;
    public object WebhookWaBusiness { get; set; } = string.Empty;
    public string AccessTokenWaBusiness { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}

public class Hash
{
    public string ApiKey { get; set; } = string.Empty;
}

public class Qrcode
{
    public object PairingCode { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Base64 { get; set; } = string.Empty;
}

public class Settings
{
    public bool RejectCall { get; set; }
    public string MsgCall { get; set; } = string.Empty;
    public bool GroupsIgnore { get; set; }
    public bool AlwaysOnline { get; set; }
    public bool ReadMessages { get; set; }
    public bool ReadStatus { get; set; }
    public bool SyncFullHistory { get; set; }
}

public class InstanceCreateResponse
{
    public Instance Instance { get; set; } = null!;
    public Hash Hash { get; set; } = null!;
    public object Webhook { get; set; } = string.Empty;
    public object Websocket { get; set; } = string.Empty;
    public object Rabbitmq { get; set; } = string.Empty;
    public object Sqs { get; set; } = string.Empty;
    public Typebot Typebot { get; set; } = null!;
    public Settings Settings { get; set; } = null!;
    public Qrcode Qrcode { get; set; } = null!;
}

public class Typebot
{
    public bool Enabled { get; set; }
}
