using ConectaAtende.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConectaAtende.Infra.Mappings;

public class TicketMapping : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.ToTable("Tickets");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Status)
               .HasConversion<int>()
               .IsRequired();

        builder.Property(t => t.Prioridade)
               .IsRequired();

        builder.Property(t => t.Descricao)
               .HasMaxLength(500);

        builder.Property(t => t.DataCriacao)
               .IsRequired();

        builder.Property(t => t.DataAlteracao);

        builder.HasOne<Contato>()                
               .WithMany()
               .HasForeignKey(t => t.ContatoId) 
               .OnDelete(DeleteBehavior.Cascade);
    }
}