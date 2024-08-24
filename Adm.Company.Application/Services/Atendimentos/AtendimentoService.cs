using Adm.Company.Application.Interfaces.Atendimento;
using Adm.Company.Application.ViewModel.Atendimentos;
using Adm.Company.Domain.Enums;
using Adm.Company.Domain.Exceptions;
using Adm.Company.Domain.Interfaces;
using Adm.Company.Infrastructure.HttpServices.Interfaces;

namespace Adm.Company.Application.Services.Atendimentos;

public sealed class AtendimentoService : IAtendimentoService
{
    private readonly IUsuarioAutenticado _usuarioAutenticado;
    private readonly IAtendimentoRepository _atendimentoRepository;
    private readonly IConfiguracaoAtendimentoEmpresaRepository _configuracaoAtendimentoEmpresaRepository;
    private readonly IMensagemAtendimentoRepository _mensagemAtendimentoRepository;

    public AtendimentoService(
        IUsuarioAutenticado usuarioAutenticado,
        IAtendimentoRepository atendimentoRepository,
        IConfiguracaoAtendimentoEmpresaRepository configuracaoAtendimentoEmpresaRepository,
        IMensagemAtendimentoRepository mensagemAtendimentoRepository)
    {
        _usuarioAutenticado = usuarioAutenticado;
        _atendimentoRepository = atendimentoRepository;
        _configuracaoAtendimentoEmpresaRepository = configuracaoAtendimentoEmpresaRepository;
        _mensagemAtendimentoRepository = mensagemAtendimentoRepository;
    }

    public async Task<IList<AtendimentoViewModel>> AtendimentosEmAbertoAsync()
    {
        var atendimentos = await _atendimentoRepository.GetAtendimentosAsync(_usuarioAutenticado.EmpresaId, StatusAtendimento.Aberto);

        return atendimentos.Select(x => (AtendimentoViewModel)x).ToList();
    }

    public async Task<AtendimentoViewModel> IniciarAtendimentoAsync(Guid atendimentoId)
    {
        var atendimento = await _atendimentoRepository.GetByIdAsync(atendimentoId)
            ?? throw new ExceptionApiErro("Não foi possível localizar o atendimento!");

        atendimento.IniciarAtendimento(_usuarioAutenticado.Id);
        await _atendimentoRepository.UpdateAsync(atendimento);

        return (AtendimentoViewModel)atendimento;
    }

    public async Task<IList<AtendimentoViewModel>> MeusAtendimentosAsync()
    {
        var atendimentos = await _atendimentoRepository
            .GetMeuAtendimentosAsync(_usuarioAutenticado.Id, _usuarioAutenticado.EmpresaId, StatusAtendimento.EmAndamento);

        var configuracaoAtendimento = await _configuracaoAtendimentoEmpresaRepository
            .GetConfiguracaoAtendimentoEmpresaByEmpresaIdAsync(_usuarioAutenticado.EmpresaId)
                ?? throw new ExceptionApiErro("Não foi possível localizar as configurações de atendimento da sua empresa!");

        var atendimentosViewModel = new List<AtendimentoViewModel>();

        foreach (var atendimento in atendimentos)
        {
            var atendimentoViewModel = (AtendimentoViewModel)atendimento;

            atendimentoViewModel.MensagensNaoLidas = await _mensagemAtendimentoRepository
                .MensagensNaoLidasAtendimentoAsync(atendimentoViewModel.Id);

            atendimentosViewModel.Add(atendimentoViewModel);
        }

        return atendimentosViewModel;
    }
}
