namespace Adm.Company.Application.ViewModel;

public abstract class BaseViewModel
{
    public Guid Id { get; protected set; }
    public DateTime CriadoEm { get; protected set; }
    public DateTime? AtualizadoEm { get; protected set; }
    public long Numero { get; protected set; }
}
