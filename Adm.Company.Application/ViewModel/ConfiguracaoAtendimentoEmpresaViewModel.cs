using Adm.Company.Domain.Entities;

namespace Adm.Company.Application.ViewModel;

public class ConfiguracaoAtendimentoEmpresaViewModel : BaseViewModel
{
    public string WhatsApp { get; set; } = string.Empty;
    public string? PrimeiraMensagem { get; set; }
    public Guid? UsuarioId { get; set; }
    public UsuarioViewModel? Usuario { get; set; }

    public static explicit operator ConfiguracaoAtendimentoEmpresaViewModel(ConfiguracaoAtendimentoEmpresa configuracao)
    {
        return new ConfiguracaoAtendimentoEmpresaViewModel()
        {
            AtualizadoEm = configuracao.AtualizadoEm,
            CriadoEm = configuracao.CriadoEm,
            Id = configuracao.Id,   
            Numero = configuracao.Numero,
            WhatsApp = configuracao.WhatsApp,
            PrimeiraMensagem = configuracao.PrimeiraMensagem,
            UsuarioId = configuracao.UsuarioId
        };
    }
}
