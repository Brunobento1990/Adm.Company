namespace Adm.Company.Application.ViewModel.WhatsApi;

public class UpdateMensagemWhatsViewModel
{
    public string Instance { get; set; } = string.Empty;
    public DataUpdateMensagemWhatsViewModel? Data { get; set; }
}

public class DataUpdateMensagemWhatsViewModel
{
    public string KeyId { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;

}
