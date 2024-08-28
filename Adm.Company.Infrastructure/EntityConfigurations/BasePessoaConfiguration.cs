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
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Cpf)
            .HasMaxLength(11);
        builder.Property(x => x.Email)
            .HasMaxLength(255);
        builder.Property(x => x.WhatsApp)
            .HasMaxLength(13);

        builder.HasIndex(x => x.Numero);
        builder.HasIndex(x => x.Email);
        builder.HasIndex(x => x.Cpf);
    }
}
