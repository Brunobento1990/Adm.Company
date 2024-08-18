using Adm.Company.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adm.Company.Infrastructure.EntityConfigurations;

public class MensagemAtendimentoConfiguration : BaseEntityConfiguration<MensagemAtendimento>
{
    public override void Configure(EntityTypeBuilder<MensagemAtendimento> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.Mensagem).IsRequired().HasMaxLength(5000);
        builder.Property(x => x.TipoMensagem).IsRequired().HasMaxLength(255);
    }
}
