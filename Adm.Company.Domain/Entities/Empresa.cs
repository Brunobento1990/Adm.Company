namespace Adm.Company.Domain.Entities;

public sealed class Empresa : BaseEntity
{
    public Empresa(
        Guid id,
        DateTime criadoEm,
        DateTime? atualizadoEm,
        long numero,
        string cnpj,
        string razaoSocial,
        string nomeFantasia)
            : base(id, criadoEm, atualizadoEm, numero)
    {
        Cnpj = cnpj;
        RazaoSocial = razaoSocial;
        NomeFantasia = nomeFantasia;
    }

    public string Cnpj { get; private set; }
    public string RazaoSocial { get; private set; }
    public string NomeFantasia { get; private set; }
    public IList<Usuario> Usuarios { get; set; } = [];
}
