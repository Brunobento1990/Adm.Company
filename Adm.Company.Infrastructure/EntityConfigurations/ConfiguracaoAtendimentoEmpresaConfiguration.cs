using Adm.Company.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adm.Company.Infrastructure.EntityConfigurations;

public class ConfiguracaoAtendimentoEmpresaConfiguration : BaseEntityConfiguration<ConfiguracaoAtendimentoEmpresa>
{
    public override void Configure(EntityTypeBuilder<ConfiguracaoAtendimentoEmpresa> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.WhatsApp)
            .IsRequired()
            .HasMaxLength(13);
    }
}
