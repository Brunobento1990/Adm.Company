namespace Adm.Company.Domain.Entities;

public abstract class BaseEntity
{
    protected BaseEntity(
        Guid id, 
        DateTime criadoEm, 
        DateTime? atualizadoEm, 
        long numero)
    {
        Id = id;
        CriadoEm = criadoEm;
        AtualizadoEm = atualizadoEm;
        Numero = numero;
    }

    public Guid Id { get; protected set; }
    public DateTime CriadoEm { get; protected set; }
    public DateTime? AtualizadoEm { get; protected set; }
    public long Numero { get; protected set; }
}
