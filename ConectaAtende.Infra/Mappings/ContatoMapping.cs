using ConectaAtende.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConectaAtende.Infra.Mappings;

public class ContatoMapping : IEntityTypeConfiguration<Contato>
{
    public void Configure(EntityTypeBuilder<Contato> builder)
    {
        builder.ToTable("Contatos");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Nome)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(c => c.Numero)
               .IsRequired()
               .HasMaxLength(20);

        builder.HasIndex(c => c.Numero)
               .IsUnique();
    }
}