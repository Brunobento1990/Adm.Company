namespace Adm.Company.Domain.Entities;

public sealed class Cliente : BasePessoa
{
    public Cliente(
        Guid id,
        DateTime criadoEm,
        DateTime? atualizadoEm,
        long numero,
        Guid empresaId,
        string cpf,
        string? whatsApp,
        string email,
        string? foto,
        string nome,
        string? remoteJid)
            : base(id, criadoEm, atualizadoEm, numero, empresaId, cpf, whatsApp, email)
    {
        Foto = foto;
        Nome = nome;
        RemoteJid = remoteJid;
    }

    public string? Foto { get; private set; }
    public string? RemoteJid { get; private set; }
    public string Nome { get; private set; }
    public IList<Atendimento> Atendimentos { get; set; } = [];
}
