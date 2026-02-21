namespace ConectaAtende.Domain.Entidades;

public class ContatoRecente
{
    public Guid Id { get; set; }
    public Guid ContatoId { get; set; }
    public DateTime DataAcesso { get; set; }

    public Contato Contato { get; set; }
    public ContatoRecente()
    {
        
    }
}


