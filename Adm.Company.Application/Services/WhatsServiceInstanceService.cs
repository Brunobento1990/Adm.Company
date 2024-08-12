using Adm.Company.Application.Interfaces;
using Adm.Company.Application.ViewModel.WhatsApi;
using Adm.Company.Domain.Exceptions;
using Adm.Company.Domain.Interfaces;
using Adm.Company.Infrastructure.HttpServices.Interfaces;
using Adm.Company.Infrastructure.HttpServices.Requests.WhtasApi;

namespace Adm.Company.Application.Services;

public sealed class WhatsServiceInstanceService : IWhatsServiceInstanceService
{
    private readonly IWhatsHttpService _whatsHttpService;
    private readonly IUsuarioAutenticado _usuarioAutenticado;

    public WhatsServiceInstanceService(IWhatsHttpService whatsHttpService, IUsuarioAutenticado usuarioAutenticado)
    {
        _whatsHttpService = whatsHttpService;
        _usuarioAutenticado = usuarioAutenticado;
    }

    public async Task<ConnectInstanceViewModel?> ConnectInstanceAsync()
    {
        var result = await _whatsHttpService.ConnectInstanceAsync(_usuarioAutenticado.Cpf);

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
            var resultConecao = await _whatsHttpService.GetConnectInstanceAsync(_usuarioAutenticado.Cpf);
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
            InstanceName = _usuarioAutenticado.Cpf,
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

    public async Task<PerfilWhatsViewModel> GetPerfilAsync()
    {
        var perfil = await _whatsHttpService.FetchInstanceAsync(_usuarioAutenticado.Cpf)
            ?? throw new ExceptionApiErro("Não foi possível obter seu perfil do whtas!");

        return new PerfilWhatsViewModel()
        {
            Foto = perfil.Instance.ProfilePictureUrl,
            Nome = perfil.Instance.ProfileName
        };
    }
}
