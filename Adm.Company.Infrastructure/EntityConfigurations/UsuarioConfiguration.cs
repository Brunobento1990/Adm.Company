using Adm.Company.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adm.Company.Infrastructure.EntityConfigurations;

public class UsuarioConfiguration : BasePessoaConfiguration<Usuario>
{
    public override void Configure(EntityTypeBuilder<Usuario> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Senha)
            .IsRequired()
            .HasMaxLength(1000);
        builder.Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(255);
        builder.Property(x => x.Bloqueado)
            .IsRequired();
    }
}
