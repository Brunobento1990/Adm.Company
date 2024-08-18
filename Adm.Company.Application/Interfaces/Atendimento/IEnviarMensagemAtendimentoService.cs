using Adm.Company.Application.Dtos.Atendimentos;
using Adm.Company.Application.ViewModel.Atendimentos;

namespace Adm.Company.Application.Interfaces.Atendimento;

public interface IEnviarMensagemAtendimentoService
{
    Task<MensagemAtendimentoViewModel> EnviarMensagemAsync(EnviarMensagemAtendimentoDto enviarMensagemAtendimentoDto);
}
