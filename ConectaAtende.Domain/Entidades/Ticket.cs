using ConectaAtende.Domain.Enums;

namespace ConectaAtende.Domain.Entidades;

public class Ticket
{
    public Guid Id { get; set; }
    public Guid ContatoId { get; set; }
    public int Prioridade { get; set; }

    public string? Descricao { get; set; }
    public StatusTicketEnum Status { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? DataAlteracao { get; set; }

    public Ticket()
    {
        
    }
}
