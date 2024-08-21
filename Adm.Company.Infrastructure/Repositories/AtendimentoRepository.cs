using Adm.Company.Domain.Entities;
using Adm.Company.Domain.Enums;
using Adm.Company.Domain.Interfaces;
using Adm.Company.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Adm.Company.Infrastructure.Repositories;

public sealed class AtendimentoRepository : IAtendimentoRepository
{
    private readonly AdmCompanyContext _admCompanyContext;

    public AtendimentoRepository(AdmCompanyContext admCompanyContext)
    {
        _admCompanyContext = admCompanyContext;
    }

    public async Task AddAsync(Atendimento atendimento)
    {
        await _admCompanyContext.AddAsync(atendimento);
        await _admCompanyContext.SaveChangesAsync();
    }

    public async Task<Atendimento?> GetAtendimentoByStatusAsync(
        StatusAtendimento statusAtendimento, 
        string numeroWhats, 
        Guid empresaId)
    {
        return await _admCompanyContext
            .Atendimentos
            .AsNoTracking()
            .Include(x => x.Mensagens)
            .Include(x => x.Cliente)
            .FirstOrDefaultAsync(x => x.Status == statusAtendimento && x.Cliente.RemoteJid == numeroWhats && x.EmpresaId == empresaId);
    }

    public async Task<Atendimento?> GetByIdAsync(Guid id)
    {
        return await _admCompanyContext
            .Atendimentos
            .AsNoTracking()
            .Include(x => x.Cliente)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IList<Atendimento>> GetMeuAtendimentosAsync(
        Guid usuarioId, 
        Guid empresaId, 
        StatusAtendimento statusAtendimento)
    {
        var result = await _admCompanyContext
            .Atendimentos
            .AsNoTracking()
            .OrderByDescending(x => x.AtualizadoEm)
                .ThenByDescending(x => x.CriadoEm)
            .Include(x => x.Cliente)
            .Where(x => x.UsuarioId == usuarioId && x.EmpresaId == empresaId)
            .Select(x => new
            {
                Atendimento = x,
                Mensagens = x.Mensagens.OrderByDescending(x => x.CriadoEm).Take(1).ToList()
            }).ToListAsync();

        return result.Select(x =>
        {
            var atendimento = x.Atendimento;
            atendimento.Mensagens = x.Mensagens;
            return atendimento;
        }).ToList();
    }
}
