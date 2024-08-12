
namespace Adm.Company.Domain.Entities;

public abstract class BaseEntityEmpresa : BaseEntity
{
    protected BaseEntityEmpresa(
        Guid id,
        DateTime criadoEm,
        DateTime? atualizadoEm,
        long numero,
        Guid empresaId)
            : base(id, criadoEm, atualizadoEm, numero)
    {
        EmpresaId = empresaId;
    }

    public Guid EmpresaId { get; protected set; }
    public Empresa Empresa { get; set; } = null!;
}
