using Adm.Company.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adm.Company.Infrastructure.EntityConfigurations;

public sealed class AtendimentoConfiguration : BaseEntityConfiguration<Atendimento>
{
    public override void Configure(EntityTypeBuilder<Atendimento> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.Observacao).HasMaxLength(255);
        builder.Property(x => x.MensagemCancelamento).HasMaxLength(255);

        builder.HasIndex(x => x.Status);

        builder.HasMany(x => x.Mensagens)
            .WithOne(x => x.Atendimento)
            .HasForeignKey(x => x.AtendimentoId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
