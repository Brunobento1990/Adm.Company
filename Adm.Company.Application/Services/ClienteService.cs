using Adm.Company.Application.Helpers;
using Adm.Company.Application.Interfaces;
using Adm.Company.Application.ViewModel;
using Adm.Company.Domain.Entities;
using Adm.Company.Domain.Interfaces;
using Adm.Company.Infrastructure.HttpServices.Requests.WhtasApi;
using static Adm.Company.Domain.Entities.Cliente;

namespace Adm.Company.Application.Services;

public sealed class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IConfiguracaoAtendimentoEmpresaRepository _configuracaoAtendimentoEmpresaRepository;
    private readonly IUsuarioAutenticado _usuarioAutenticado;

    public ClienteService(
        IClienteRepository clienteRepository,
        IConfiguracaoAtendimentoEmpresaRepository configuracaoAtendimentoEmpresaRepository,
        IUsuarioAutenticado usuarioAutenticado)
    {
        _clienteRepository = clienteRepository;
        _configuracaoAtendimentoEmpresaRepository = configuracaoAtendimentoEmpresaRepository;
        _usuarioAutenticado = usuarioAutenticado;
    }

    public async Task AddClientesFromWhatsAsync(UpdateContactRequest updateContactRequest)
    {
        try
        {
            var configuracao = await _configuracaoAtendimentoEmpresaRepository
            .GetConfiguracaoAtendimentoEmpresaByNumeroWhtasAsync(numeroWhats: updateContactRequest.Instance);

            if (configuracao == null)
            {
                return;
            }

            var clientes = new List<Cliente>();
            var clientesUpdate = new List<Cliente>();
            var listaFiltrada = updateContactRequest.Data
                .Where(x => !string.IsNullOrWhiteSpace(x.RemoteJid) && !string.IsNullOrWhiteSpace(x.PushName))
                .ToList();

            foreach (var clienteDto in listaFiltrada)
            {
                if (clienteDto.RemoteJid != null &&
                    clienteDto.RemoteJid.Contains("whatsapp.net") &&
                    !string.IsNullOrWhiteSpace(clienteDto.PushName))
                {
                    var cliente = await _clienteRepository
                    .GetByRemoteJidWhatsAsync(remoteJid: clienteDto.RemoteJid, empresaId: configuracao.EmpresaId);

                    if (cliente == null)
                    {
                        var numeroWhatsTratado = ConvertWhatsHelpers.ConvertRemoteJidWhats(clienteDto.RemoteJid);

                        clientes.Add(FactorieCliente.FactorieWhats(
                            empresaId: configuracao.EmpresaId,
                            numeroWhats: numeroWhatsTratado,
                            foto: clienteDto.ProfilePicUrl,
                            nome: clienteDto.PushName,
                            remoteJid: clienteDto.RemoteJid));

                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(cliente.Foto) && !string.IsNullOrWhiteSpace(clienteDto.ProfilePicUrl))
                    {
                        cliente.UpdateFoto(clienteDto.ProfilePicUrl);
                        clientesUpdate.Add(cliente);
                    }
                }
            }

            await _clienteRepository.UpdateRangeAsync(clientesUpdate);
            await _clienteRepository.AddRangeAsync(clientes);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async Task<IList<ClienteViewModel>> PaginacaoAsync()
    {
        var clientes = await _clienteRepository.GetPaginacaoAsync(_usuarioAutenticado.EmpresaId);

        return clientes.Select(x => (ClienteViewModel)x).ToList();
    }
}
