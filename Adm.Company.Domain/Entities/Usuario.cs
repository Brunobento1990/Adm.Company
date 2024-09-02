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
        bool bloqueado,
        string nome)
            : base(id, criadoEm, atualizadoEm, numero, empresaId, cpf, whatsApp, email)
    {
        Senha = senha;
        Bloqueado = bloqueado;
        Nome = nome;
    }

    public string Senha { get; set; }
    public string Nome { get; set; }
    public bool Bloqueado { get; set; }
}
