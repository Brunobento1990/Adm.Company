using Adm.Company.Application.Interfaces;
using Adm.Company.Application.ViewModel.WhatsApi;
using Adm.Company.Domain.Entities;
using Adm.Company.Domain.Exceptions;
using Adm.Company.Domain.Interfaces;
using Adm.Company.Infrastructure.HttpServices.Interfaces;
using Adm.Company.Infrastructure.HttpServices.Requests.WhtasApi;

namespace Adm.Company.Application.Services;

public sealed class WhatsServiceInstanceService : IWhatsServiceInstanceService
{
    private readonly IWhatsHttpService _whatsHttpService;
    private readonly IEmpresaAutenticada _empresaAutenticada;
    private readonly IConfiguracaoAtendimentoEmpresaRepository _configuracaoAtendimentoEmpresaRepository;

    public WhatsServiceInstanceService(IWhatsHttpService whatsHttpService, IEmpresaAutenticada empresaAutenticada, IConfiguracaoAtendimentoEmpresaRepository configuracaoAtendimentoEmpresaRepository)
    {
        _whatsHttpService = whatsHttpService;
        _empresaAutenticada = empresaAutenticada;
        _configuracaoAtendimentoEmpresaRepository = configuracaoAtendimentoEmpresaRepository;
    }

    async Task<ConfiguracaoAtendimentoEmpresa> GetConfiguracaoAtendimentoEmpresaAsync()
    {
        return await _configuracaoAtendimentoEmpresaRepository.GetConfiguracaoAtendimentoEmpresaByEmpresaIdAsync(_empresaAutenticada.Id)
            ?? throw new ExceptionApiErro("A configuração do whtas não foi localizada!");
    }

    public async Task<ConnectInstanceViewModel?> ConnectInstanceAsync()
    {
        var configuracaoWhats = await GetConfiguracaoAtendimentoEmpresaAsync();
        var result = await _whatsHttpService.ConnectInstanceAsync(configuracaoWhats.WhatsApp);

        if (result != null && result.Instance != null && (result.Instance.State == "connected" || result.Instance.State == "open"))
        {
            return new()
            {
                QrCode = string.Empty,
                Status = 200
            };
        }

        if (result != null && result.Instance != null && (result.Instance.State == "connecting" || result.Instance.State == "close"))
        {
            var resultConecao = await _whatsHttpService.GetConnectInstanceAsync(configuracaoWhats.WhatsApp);
            if (resultConecao != null)
            {
                return new()
                {
                    QrCode = resultConecao.Base64,
                    Status = 201
                };
            }
        }

        var resultCreate = await _whatsHttpService.CreateInstanceAsync(new CreateInstanceResponse()
        {
            InstanceName = configuracaoWhats.WhatsApp,
            Qrcode = true,
            Token = Guid.NewGuid().ToString()
        });

        if (resultCreate != null)
        {
            return new()
            {
                QrCode = resultCreate.Qrcode.Base64,
                Status = 201
            };
        }

        return null;
    }
}
