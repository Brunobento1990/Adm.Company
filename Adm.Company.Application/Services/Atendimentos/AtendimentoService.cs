using Adm.Company.Application.Dtos.Atendimentos;
using Adm.Company.Application.Interfaces.Atendimentos;
using Adm.Company.Application.ViewModel.Atendimentos;
using Adm.Company.Domain.Enums;
using Adm.Company.Domain.Exceptions;
using Adm.Company.Domain.Interfaces;
using static Adm.Company.Domain.Entities.Atendimento;

namespace Adm.Company.Application.Services.Atendimentos;

public sealed class AtendimentoService : IAtendimentoService
{
    private readonly IUsuarioAutenticado _usuarioAutenticado;
    private readonly IAtendimentoRepository _atendimentoRepository;
    private readonly IConfiguracaoAtendimentoEmpresaRepository _configuracaoAtendimentoEmpresaRepository;
    private readonly IMensagemAtendimentoRepository _mensagemAtendimentoRepository;
    private readonly IClienteRepository _clienteRepository;

    public AtendimentoService(
        IUsuarioAutenticado usuarioAutenticado,
        IAtendimentoRepository atendimentoRepository,
        IConfiguracaoAtendimentoEmpresaRepository configuracaoAtendimentoEmpresaRepository,
        IMensagemAtendimentoRepository mensagemAtendimentoRepository,
        IClienteRepository clienteRepository)
    {
        _usuarioAutenticado = usuarioAutenticado;
        _atendimentoRepository = atendimentoRepository;
        _configuracaoAtendimentoEmpresaRepository = configuracaoAtendimentoEmpresaRepository;
        _mensagemAtendimentoRepository = mensagemAtendimentoRepository;
        _clienteRepository = clienteRepository;
    }

    public async Task<IList<AtendimentoViewModel>> AtendimentosEmAbertoAsync()
    {
        var atendimentos = await _atendimentoRepository.GetAtendimentosAsync(_usuarioAutenticado.EmpresaId, StatusAtendimento.Aberto);

        return atendimentos.Select(x => (AtendimentoViewModel)x).ToList();
    }

    public async Task CancelarAtendimentoAsync(CancelarAtendimentoDto cancelarAtendimentoDto)
    {
        cancelarAtendimentoDto.Validar();

        var atendimento = await _atendimentoRepository.GetByIdAsync(cancelarAtendimentoDto.AtendimentoId)
            ?? throw new ExceptionApiErro("Não foi possível localizar o atendimento!");

        if (atendimento.Status == StatusAtendimento.Cancelado)
        {
            throw new ExceptionApiErro("O atendimento se encontra cancelado!");
        }
        if (atendimento.Status == StatusAtendimento.Fechado)
        {
            throw new ExceptionApiErro("O atendimento se encontra fechado!");
        }

        atendimento.CancelarAtendimento(
            usuarioId: _usuarioAutenticado.Id,
            motivoCancelamento: cancelarAtendimentoDto.MotivoCancelamento,
            observacao: cancelarAtendimentoDto.Observacao);

        await _atendimentoRepository.UpdateAsync(atendimento);
    }

    public async Task FinalizarAsync(FinalizarAtendimentoDto finalizarAtendimentoDto)
    {
        finalizarAtendimentoDto.Validar();

        var atendimento = await _atendimentoRepository.GetByIdAsync(finalizarAtendimentoDto.AtendimentoId)
            ?? throw new ExceptionApiErro("Não foi possível localizar o atendimento!");

        if (atendimento.Status == StatusAtendimento.Cancelado)
        {
            throw new ExceptionApiErro("O atendimento se encontra cancelado!");
        }
        if (atendimento.Status == StatusAtendimento.Fechado)
        {
            throw new ExceptionApiErro("O atendimento se encontra fechado!");
        }

        atendimento.FinalizarAtendimento(
            usuarioId: _usuarioAutenticado.Id,
            observacao: finalizarAtendimentoDto.Observacao);

        await _atendimentoRepository.UpdateAsync(atendimento);
    }

    public async Task<AtendimentoViewModel> IniciarAtendimentoAsync(Guid atendimentoId)
    {
        var atendimento = await _atendimentoRepository.GetByIdAsync(atendimentoId)
            ?? throw new ExceptionApiErro("Não foi possível localizar o atendimento!");

        if (atendimento.UsuarioId.HasValue)
        {
            throw new ExceptionApiErro("Este atendimento já se encontra com outro usuário!");
        }

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

    public async Task<AtendimentoViewModel> NovoAtendimentoAsync(Guid clienteId)
    {
        var cliente = await _clienteRepository.GetByIdAsync(id: clienteId, empresaId: _usuarioAutenticado.EmpresaId)
            ?? throw new ExceptionApiErro("Não foi possível localizar o cliente!");

        var atendimento = await _atendimentoRepository
            .GetAtendimentoEmAbertoByUsuarioIdAsync(clienteId: clienteId, empresaId: _usuarioAutenticado.EmpresaId);

        if (atendimento != null)
        {
            if (atendimento.Status == StatusAtendimento.Aberto)
            {
                throw new ExceptionApiErro($"Já existe um atendimento em aberto para: {atendimento.Cliente.Nome}");
            }

            throw new ExceptionApiErro($"Já existe um atendimento em andamento para: {atendimento.Cliente.Nome}");
        }

        atendimento = Factorie.NovoAtendimento(
            empresaId: _usuarioAutenticado.EmpresaId,
            clienteId: clienteId,
            usuarioId: _usuarioAutenticado.Id);

        await _atendimentoRepository.AddAsync(atendimento);

        atendimento.Cliente = cliente;
        return (AtendimentoViewModel)atendimento;
    }
}
