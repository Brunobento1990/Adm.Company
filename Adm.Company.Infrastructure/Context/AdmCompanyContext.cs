using Adm.Company.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Adm.Company.Infrastructure.Context;

public class AdmCompanyContext : DbContext
{
    public AdmCompanyContext(DbContextOptions<AdmCompanyContext> options) : base(options)
    {
    }

    public DbSet<Usuario> Usuarios {  get; set; }
    public DbSet<Empresa> Empresas {  get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
