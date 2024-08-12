namespace Adm.Company.Domain.Entities;

public sealed class Usuario : BasePessoa
{
    public Usuario(
        Guid id,
        DateTime criadoEm,
        DateTime? atualizadoEm,
        long numero,
        Guid empresaId,
        string cpf,
        string? whatsApp,
        string email,
        string senha,
        bool bloqueado)
            : base(id, criadoEm, atualizadoEm, numero, empresaId, cpf, whatsApp, email)
    {
        Senha = senha;
        Bloqueado = bloqueado;
    }

    public string Senha { get; set; }
    public bool Bloqueado { get; set; }
}
