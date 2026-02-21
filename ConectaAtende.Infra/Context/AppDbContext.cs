using ConectaAtende.Domain.Entidades;
using ConectaAtende.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace ConectaAtende.Infra.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Contato> Contatos { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<ContatoRecente> ContatosRecentes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Contatos
        modelBuilder.Entity<Contato>().HasData(
            new Contato
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Nome = "João Silva",
                Numero = "11999999999",
                DataCriacao = DateTime.UtcNow,
                DataAtualizacao = DateTime.UtcNow
            },
            new Contato
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Nome = "Maria Souza",
                Numero = "11888888888",
                DataCriacao = DateTime.UtcNow,
                DataAtualizacao = DateTime.UtcNow
            }
        );

        // Clientes
        modelBuilder.Entity<Cliente>().HasData(
            new Cliente
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Nome = "Carlos",
                Idade = 30
            }
        );

        // Ticket
        modelBuilder.Entity<Ticket>().HasData(
            new Ticket
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                ContatoId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Descricao = "Problema com o produto",
                Prioridade = 1,
                Status = StatusTicketEnum.Criado,
                DataCriacao = DateTime.UtcNow
            }
        );
    }
}