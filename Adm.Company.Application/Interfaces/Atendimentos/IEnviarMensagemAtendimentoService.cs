using Adm.Company.Application.Dtos.Atendimentos;
using Adm.Company.Application.ViewModel.Atendimentos;
using Adm.Company.Domain.Entities;

namespace Adm.Company.Application.Interfaces.Atendimentos;

public interface IEnviarMensagemAtendimentoService
{
    Task<MensagemAtendimentoViewModel> EnviarMensagemAsync(EnviarMensagemAtendimentoDto enviarMensagemAtendimentoDto);
    Task<MensagemAtendimentoViewModel> EnviarPrimeiraMensagemMensagemAsync(string remoteJid,
        ConfiguracaoAtendimentoEmpresa configuracaoAtendimento, Atendimento atendimento);
}
