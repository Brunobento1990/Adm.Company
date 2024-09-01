using Adm.Company.Application.Dtos.Atendimentos;
using Adm.Company.Application.ViewModel.Atendimentos;

namespace Adm.Company.Application.Interfaces.Atendimento;

public interface IAtendimentoService
{
    Task<IList<AtendimentoViewModel>> MeusAtendimentosAsync();
    Task<IList<AtendimentoViewModel>> AtendimentosEmAbertoAsync();
    Task<AtendimentoViewModel> IniciarAtendimentoAsync(Guid atendimentoId);
    Task<AtendimentoViewModel> NovoAtendimentoAsync(Guid clienteId);
    Task CancelarAtendimentoAsync(CancelarAtendimentoDto cancelarAtendimentoDto);
    Task FinalizarAsync(FinalizarAtendimentoDto finalizarAtendimentoDto);
}
