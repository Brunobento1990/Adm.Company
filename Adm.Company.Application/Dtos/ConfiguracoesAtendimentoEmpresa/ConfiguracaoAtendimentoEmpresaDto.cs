namespace Adm.Company.Application.Dtos.ConfiguracoesAtendimentoEmpresa;

public class ConfiguracaoAtendimentoEmpresaDto
{
    public string NumeroWhats { get; set; } = string.Empty;
    public string? PrimeiraMensagem { get; set; }
    public Guid? UsuarioId { get; set; }
}
