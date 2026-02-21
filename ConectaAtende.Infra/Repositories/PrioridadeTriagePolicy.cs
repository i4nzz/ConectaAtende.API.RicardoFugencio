using ConectaAtende.Application.Interfaces;
using ConectaAtende.Domain.Entidades;
using ConectaAtende.Domain.Enums;
using ConectaAtende.Domain.Interfaces;

namespace ConectaAtende.Application.Policies;

public class PrioridadeTriagePolicy : ITriagePolicy
{
    public Ticket? SelecionarProximo(IEnumerable<Ticket> tickets)
    {
        return tickets
            .Where(t => t.Status == StatusTicketEnum.Criado)
            .OrderByDescending(t => t.Prioridade)
            .ThenBy(t => t.DataCriacao)
            .FirstOrDefault();
    }
}