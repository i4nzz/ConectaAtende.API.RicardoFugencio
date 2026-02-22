using ConectaAtende.Domain.Entidades;
using ConectaAtende.Domain.Enums;
using ConectaAtende.Domain.Interfaces;

namespace ConectaAtende.Application.Policies;

public class OrdemChegadaTriagePolicy : ITriagePolicy
{
    public Ticket? SelecionarProximo(IEnumerable<Ticket> tickets)
    {
        return tickets
            .Where(t => t.Status == StatusTicketEnum.NaFila)
            .OrderBy(t => t.DataCriacao)
            .FirstOrDefault();
    }
}
