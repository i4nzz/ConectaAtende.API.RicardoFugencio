namespace ConectaAtende.Application.DTO.TicketDTO;

public class CreateTicketRequest
{
    public Guid ContatoId { get; set; }
    public string? Descricao { get; set; }
    public int Prioridade { get; set; }
}
