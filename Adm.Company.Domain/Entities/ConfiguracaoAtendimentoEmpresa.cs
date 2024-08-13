namespace Adm.Company.Domain.Entities;

public sealed class ConfiguracaoAtendimentoEmpresa : BaseEntityEmpresa
{
    public ConfiguracaoAtendimentoEmpresa(
        Guid id,
        DateTime criadoEm,
        DateTime? atualizadoEm,
        long numero,
        Guid empresaId,
        string whatsApp)
            : base(id, criadoEm, atualizadoEm, numero, empresaId)
    {
        WhatsApp = whatsApp;
    }

    public string WhatsApp { get; private set; }
}
