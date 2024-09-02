namespace Adm.Company.Domain.Entities;

public sealed class ConfiguracaoAtendimentoEmpresa : BaseEntityEmpresa
{
    public ConfiguracaoAtendimentoEmpresa(
        Guid id,
        DateTime criadoEm,
        DateTime? atualizadoEm,
        long numero,
        Guid empresaId,
        string whatsApp,
        string? primeiraMensagem,
        Guid? usuarioId)
            : base(id, criadoEm, atualizadoEm, numero, empresaId)
    {
        WhatsApp = whatsApp;
        PrimeiraMensagem = primeiraMensagem;
        UsuarioId = usuarioId;
    }

    public string WhatsApp { get; private set; }
    public string? PrimeiraMensagem { get; private set; }
    public Guid? UsuarioId { get; private set; }

    public void Update(string whatsApp, string? primeiraMensagem, Guid? usuarioId)
    {
        WhatsApp = whatsApp;
        PrimeiraMensagem = primeiraMensagem;
        UsuarioId = usuarioId;
    }
}
