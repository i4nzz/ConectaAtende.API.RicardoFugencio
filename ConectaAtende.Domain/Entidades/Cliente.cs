namespace ConectaAtende.Domain.Entidades;

public class Cliente
{
    public Guid Id { get; set; }
    public string? Nome { get; set; }
    public int Idade { get; set; }
    public Cliente()
    {
        
    }
    public Cliente(string nome, int idade)
    {
        Nome = nome;
        Idade = idade;
    }
}
