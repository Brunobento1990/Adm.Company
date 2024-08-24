namespace Adm.Company.Application.Dtos.Atendimentos;

public class EnviarMensagemAtendimentoDto
{
    public Guid AtendimentoId { get; set; }
    public string Mensagem { get; set; } = string.Empty;
    public string? Audio { get; set; }
}
