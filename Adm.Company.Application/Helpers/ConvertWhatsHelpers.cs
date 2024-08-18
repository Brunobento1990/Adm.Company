using Adm.Company.Domain.Enums;

namespace Adm.Company.Application.Helpers;

public class ConvertWhatsHelpers
{
    public static string ConvertRemoteJidWhats(string remoteJid)
    {
        if (string.IsNullOrWhiteSpace(remoteJid)) return string.Empty;

        var primeiraConversao = remoteJid.Split('@');
        if(primeiraConversao != null)
        {
            var segundaConversao = primeiraConversao[0][2..];
            var numero = segundaConversao[2..];
            var ddd = segundaConversao[..2];
            return $"{ddd}9{numero}";
        }

        return string.Empty;
    }

    public static StatusMensagem? ConvertStatus(string statusMensagem)
    {
        if (string.IsNullOrWhiteSpace(statusMensagem))
        {
            return null;
        }

        if(statusMensagem == "DELIVERY_ACK")
        {
            return StatusMensagem.Entregue;
        }

        if(statusMensagem == "READ")
        {
            return StatusMensagem.Lida;
        }

        return null;
    }
}
