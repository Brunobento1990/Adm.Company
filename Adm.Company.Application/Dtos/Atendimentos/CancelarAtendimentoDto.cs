using Adm.Company.Domain.Exceptions;

namespace Adm.Company.Application.Dtos.Atendimentos;

public class CancelarAtendimentoDto
{
    public Guid AtendimentoId { get; set; }
    public string? Observacao { get; set; }
    public string MotivoCancelamento { get; set; } = string.Empty;

    public void Validar()
    {
        if(AtendimentoId == Guid.Empty)
        {
            throw new ExceptionApiErro("Atendimento inválido!");
        }

        if (string.IsNullOrWhiteSpace(MotivoCancelamento))
        {
            throw new ExceptionApiErro("Informe o motivo do cancelamento!");
        }

        if(MotivoCancelamento.Length > 255)
        {
            throw new ExceptionApiErro("Informe no máximo 255 caracteres para o motivo do cancelamento!");
        }

        if (!string.IsNullOrWhiteSpace(Observacao) && Observacao.Length > 255)
        {
            throw new ExceptionApiErro("Informe no máximo 255 caracteres para a observação!");
        }
    }
}
