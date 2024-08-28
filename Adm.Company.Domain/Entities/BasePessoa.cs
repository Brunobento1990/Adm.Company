namespace Adm.Company.Domain.Entities;

public abstract class BasePessoa : BaseEntityEmpresa
{
    public BasePessoa(
        Guid id,
        DateTime criadoEm,
        DateTime? atualizadoEm,
        long numero,
        Guid empresaId,
        string? cpf,
        string? whatsApp,
        string? email) : base(id, criadoEm, atualizadoEm, numero, empresaId)
    {
        Cpf = cpf;
        WhatsApp = whatsApp;
        Email = email;
    }

    public string? Cpf { get; protected set; }
    public string? Email { get; protected set; }
    public string? WhatsApp { get; protected set; }
}
