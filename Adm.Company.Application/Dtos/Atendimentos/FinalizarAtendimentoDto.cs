using Adm.Company.Domain.Exceptions;

namespace Adm.Company.Application.Dtos.Atendimentos;

public class FinalizarAtendimentoDto
{
    public Guid AtendimentoId { get; set; }
    public string? Observacao { get; set; }

    public void Validar()
    {
        if (AtendimentoId == Guid.Empty)
        {
            throw new ExceptionApiErro("Atendimento inválido!");
        }

        if (!string.IsNullOrWhiteSpace(Observacao) && Observacao.Length > 255)
        {
            throw new ExceptionApiErro("Informe no máximo 255 caracteres para a observação!");
        }
    }
}
