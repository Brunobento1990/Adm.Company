using Adm.Company.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adm.Company.Infrastructure.EntityConfigurations;

public class EmpresaConfiguration : BaseEntityConfiguration<Empresa>
{
    public override void Configure(EntityTypeBuilder<Empresa> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Cnpj)
            .IsRequired()
            .HasMaxLength(14);
        builder.Property(x => x.RazaoSocial)
            .IsRequired()
            .HasMaxLength(255);
        builder.Property(x => x.NomeFantasia)
            .IsRequired()
            .HasMaxLength(255);
        builder.HasIndex(x => x.Cnpj)
            .IsUnique();
        builder.HasMany(x => x.Usuarios)
            .WithOne(x => x.Empresa)
            .HasForeignKey(x => x.EmpresaId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
