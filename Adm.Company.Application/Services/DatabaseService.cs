using Adm.Company.Application.Interfaces;
using Adm.Company.Domain.Entities;
using Adm.Company.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Adm.Company.Application.Services;

public sealed class DatabaseService : IDatabaseService
{
    private readonly AdmCompanyContext _admCompanyContext;

    public DatabaseService(AdmCompanyContext admCompanyContext)
    {
        _admCompanyContext = admCompanyContext;
    }

    public async Task CriarDbDevAsync()
    {
        await _admCompanyContext.Database.MigrateAsync();

        var empresa = await _admCompanyContext
            .Empresas
            .FirstOrDefaultAsync(x => x.NomeFantasia == "Dev");

        if (empresa == null) 
        {
            empresa = new Empresa(
                id: Guid.NewGuid(),
                criadoEm: DateTime.Now,
                atualizadoEm: null,
                numero:0,
                cnpj: "123",
                razaoSocial: "Dev",
                nomeFantasia: "Dev");

            empresa.Usuarios.Add(new Usuario(
                id: Guid.NewGuid(),
                criadoEm: DateTime.Now,
                atualizadoEm: DateTime.Now,
                numero: 0,
                empresaId: empresa.Id,
                cpf: "123456789",
                whatsApp: null,
                email: "dev@gmail.com",
                senha: "$2a$10$NqR07ERdl.v32pQJJemrXu45nPMcZnY9WvaGX1winoU5vnOYVjoRG",
                bloqueado: false,
                nome: "dev"));

            await _admCompanyContext.AddAsync(empresa);
            await _admCompanyContext.SaveChangesAsync();
        }
    }
}
