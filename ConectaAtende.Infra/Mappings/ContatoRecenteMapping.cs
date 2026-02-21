using ConectaAtende.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConectaAtende.Infra.Mappings;

public class ContatoRecenteMapping : IEntityTypeConfiguration<ContatoRecente>
{
    public void Configure(EntityTypeBuilder<ContatoRecente> builder)
    {
        builder.ToTable("ContatosRecentes");

        builder.HasKey(cr => cr.Id);

        builder.Property(cr => cr.DataAcesso)
               .IsRequired();

        builder.HasOne(cr => cr.Contato)
               .WithMany()
               .HasForeignKey(cr => cr.ContatoId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(cr => cr.DataAcesso);
    }
}