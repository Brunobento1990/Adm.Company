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
    private readonly IChatWhatsHttpService _chatWhatsHttpService;
    private readonly IConfiguracaoAtendimentoEmpresaRepository _configuracaoAtendimentoEmpresaRepository;

    public AtendimentoService(
        IUsuarioAutenticado usuarioAutenticado,
        IAtendimentoRepository atendimentoRepository,
        IChatWhatsHttpService chatWhatsHttpService,
        IConfiguracaoAtendimentoEmpresaRepository configuracaoAtendimentoEmpresaRepository)
    {
        _usuarioAutenticado = usuarioAutenticado;
        _atendimentoRepository = atendimentoRepository;
        _chatWhatsHttpService = chatWhatsHttpService;
        _configuracaoAtendimentoEmpresaRepository = configuracaoAtendimentoEmpresaRepository;
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
            if (!string.IsNullOrWhiteSpace(atendimento.Cliente.RemoteJid))
            {
                var responsePerfil = await _chatWhatsHttpService
                    .GetPerfilClienteAsync(configuracaoAtendimento.WhatsApp, atendimento.Cliente.RemoteJid);

                atendimentoViewModel.Cliente.Foto = responsePerfil?.ProfilePictureUrl;
            }
            atendimentosViewModel.Add(atendimentoViewModel);
        }

        return atendimentosViewModel;
    }
}
