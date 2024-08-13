using Adm.Company.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adm.Company.Infrastructure.EntityConfigurations;

public abstract class BasePessoaConfiguration<T> : IEntityTypeConfiguration<T> where T : BasePessoa
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CriadoEm)
            .IsRequired()
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("now()");
        builder.Property(x => x.AtualizadoEm)
            .ValueGeneratedOnUpdate();
        builder.Property(x => x.Numero)
            .IsRequired();

        builder.Property(x => x.Cpf)
            .IsRequired()
            .HasMaxLength(11);
        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(255);
        builder.Property(x => x.WhatsApp)
            .HasMaxLength(13);

        builder.HasIndex(x => x.Numero);
        builder.HasIndex(x => x.Email);
        builder.HasIndex(x => x.Cpf);
    }
}
