using Adm.Company.Domain.Exceptions;

namespace Adm.Company.Application.Dtos.ConfiguracoesAtendimentoEmpresa;

public class ConfiguracaoAtendimentoEmpresaDto
{
    public string WhatsApp { get; set; } = string.Empty;
    public string? PrimeiraMensagem { get; set; }
    public Guid? UsuarioId { get; set; }

    public void Validar()
    {
        if (string.IsNullOrWhiteSpace(WhatsApp) || WhatsApp.Length > 13)
        {
            throw new ExceptionApiErro("Número whats inválido!");
        }

        if (PrimeiraMensagem?.Length > 255)
        {
            throw new ExceptionApiErro("A primeira mensagem deve conter no máximo 255 caracteres!");
        }
    }
}
