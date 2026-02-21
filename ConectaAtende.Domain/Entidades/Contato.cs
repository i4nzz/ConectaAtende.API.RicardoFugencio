namespace ConectaAtende.Domain.Entidades;

public class Contato
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Numero { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime DataAtualizacao { get; set; }
    public Contato()
    {
        
    }

    public Contato(string numero)
    {
        Numero = numero;
        DataCriacao = DateTime.Now;
        DataAtualizacao = DateTime.Now;
    }
}
