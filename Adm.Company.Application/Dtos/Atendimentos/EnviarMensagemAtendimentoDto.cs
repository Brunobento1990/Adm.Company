namespace Adm.Company.Application.Dtos.Atendimentos;

public class EnviarMensagemAtendimentoDto
{
    public Guid AtendimentoId { get; set; }
    public string Mensagem { get; set; } = string.Empty;
    public string? Audio { get; set; }
    public string? Imagem { get; set; }
    public string? Figurinha { get; set; }
    public string? Resposta { get; set; }
    public Guid? RespostaId { get; set; }
}
