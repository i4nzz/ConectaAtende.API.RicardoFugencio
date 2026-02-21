using ConectaAtende.Domain.Entidades;

namespace ConectaAtende.Domain.Interfaces;

public interface ITriagePolicy
{
    Ticket? SelecionarProximo(IEnumerable<Ticket> tickets);
}
