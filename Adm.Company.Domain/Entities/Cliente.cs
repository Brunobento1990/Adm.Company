namespace Adm.Company.Domain.Entities;

public sealed class Cliente : BasePessoa
{
    public Cliente(
        Guid id,
        DateTime criadoEm,
        DateTime? atualizadoEm,
        long numero,
        Guid empresaId,
        string? cpf,
        string? whatsApp,
        string? email,
        string? foto,
        string nome,
        string? remoteJid)
            : base(id, criadoEm, atualizadoEm, numero, empresaId, cpf, whatsApp, email)
    {
        Foto = foto;
        Nome = nome;
        RemoteJid = remoteJid;
    }

    public void UpdateFoto(string foto)
    {
        Foto = foto;
    }

    public string? Foto { get; private set; }
    public string? RemoteJid { get; private set; }
    public string Nome { get; private set; }
    public IList<Atendimento> Atendimentos { get; set; } = [];

    public static class FactorieCliente
    {
        public static Cliente FactorieWhats(
            Guid empresaId,
            string numeroWhats,
            string? foto,
            string nome,
            string remoteJid)
        {
            return new Cliente(
                            id: Guid.NewGuid(),
                            criadoEm: DateTime.Now,
                            atualizadoEm: DateTime.Now,
                            numero: 0,
                            empresaId: empresaId,
                            cpf: null,
                            whatsApp: numeroWhats,
                            email: null,
                            foto: foto,
                            nome: nome.Length > 255 ? nome[..244] : nome,
                            remoteJid: remoteJid);
        }
    }
}
