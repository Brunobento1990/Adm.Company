using Adm.Company.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adm.Company.Infrastructure.EntityConfigurations;

public class ClienteConfiguration : BasePessoaConfiguration<Cliente>
{
    public override void Configure(EntityTypeBuilder<Cliente> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Foto).HasMaxLength(1000);
        builder.Property(x => x.Nome).HasMaxLength(255);
        builder.Property(x => x.RemoteJid).HasMaxLength(50);
        builder.HasIndex(x => x.WhatsApp);
        builder.HasIndex(x => x.RemoteJid);
        builder.HasMany(x => x.Atendimentos)
            .WithOne(x => x.Cliente)
            .HasForeignKey(x => x.ClienteId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
