using Adm.Company.Application.Dtos.Atendimentos;
using Adm.Company.Application.Interfaces.Atendimento;
using Adm.Company.Application.ViewModel.Atendimentos;
using Adm.Company.Domain.Entities;
using Adm.Company.Domain.Enums;
using Adm.Company.Domain.Exceptions;
using Adm.Company.Domain.Interfaces;
using Adm.Company.Infrastructure.HttpServices.Interfaces;
using Adm.Company.Infrastructure.HttpServices.Requests.WhtasApi;

namespace Adm.Company.Application.Services.Atendimentos;

public sealed class EnviarMensagemAtendimentoService : IEnviarMensagemAtendimentoService
{
    private readonly IAtendimentoRepository _atendimentoRepository;
    private readonly IConfiguracaoAtendimentoEmpresaRepository _configuracaoAtendimentoEmpresaRepository;
    private readonly IUsuarioAutenticado _usuarioAutenticado;
    private readonly IMensagemAtendimentoRepository _mensagemAtendimentoRepository;
    private readonly IChatWhatsHttpService _chatWhatsHttpService;

    public EnviarMensagemAtendimentoService(
        IAtendimentoRepository atendimentoRepository,
        IConfiguracaoAtendimentoEmpresaRepository configuracaoAtendimentoEmpresaRepository,
        IUsuarioAutenticado usuarioAutenticado,
        IMensagemAtendimentoRepository mensagemAtendimentoRepository,
        IChatWhatsHttpService chatWhatsHttpService)
    {
        _atendimentoRepository = atendimentoRepository;
        _configuracaoAtendimentoEmpresaRepository = configuracaoAtendimentoEmpresaRepository;
        _usuarioAutenticado = usuarioAutenticado;
        _mensagemAtendimentoRepository = mensagemAtendimentoRepository;
        _chatWhatsHttpService = chatWhatsHttpService;
    }

    public async Task<MensagemAtendimentoViewModel> EnviarMensagemAsync(EnviarMensagemAtendimentoDto enviarMensagemAtendimentoDto)
    {
        var atendimento = await _atendimentoRepository.GetByIdAsync(enviarMensagemAtendimentoDto.AtendimentoId)
            ?? throw new ExceptionApiErro("Não foi possível localizar o atendimento!");
        var configuracaoAtendimento = await _configuracaoAtendimentoEmpresaRepository.GetConfiguracaoAtendimentoEmpresaByEmpresaIdAsync(_usuarioAutenticado.EmpresaId)
            ?? throw new ExceptionApiErro("Não foi possível localizar as configurações de atendimento!");

        var result = await _chatWhatsHttpService.EnviarMensagemAsync(configuracaoAtendimento.WhatsApp, new EnviarMensagemRequest()
        {
            Number = atendimento.Cliente.RemoteJid ?? "",
            TextMessage = new()
            {
                Text = enviarMensagemAtendimentoDto.Mensagem
            }
        });

        if (result == null || result.Key == null)
        {
            throw new ExceptionApiErro("Não foi possível enviar a mensagem, tente novamente!");
        }

        var mensagemAtendimento = new MensagemAtendimento(
            id: Guid.NewGuid(),
            criadoEm: DateTime.Now,
            atualizadoEm: DateTime.Now,
            numero: 0,
            mensagem: enviarMensagemAtendimentoDto.Mensagem,
            atendimentoId: atendimento.Id,
            status: StatusMensagem.Enviado,
            tipoMensagem: "conversation",
            remoteId: result.Key.Id,
            minhaMensagem: true);

        await _mensagemAtendimentoRepository.AddAsync(mensagemAtendimento);

        return (MensagemAtendimentoViewModel)mensagemAtendimento;
    }
}
