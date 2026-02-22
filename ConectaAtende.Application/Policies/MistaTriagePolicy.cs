using ConectaAtende.Domain.Entidades;
using ConectaAtende.Domain.Enums;
using ConectaAtende.Domain.Interfaces;

namespace ConectaAtende.Application.Policies;

public class MistaTriagePolicy : ITriagePolicy
{
    public Ticket? SelecionarProximo(IEnumerable<Ticket> tickets)
    {
        var ticketsCandidatos = tickets.Where(t => t.Status == StatusTicketEnum.NaFila);

        var comPrioridade = ticketsCandidatos.Where(t => t.Prioridade > 0);
        if (comPrioridade.Any())
        {
            return comPrioridade
                .OrderByDescending(t => t.Prioridade)
                .ThenBy(t => t.DataCriacao)
                .FirstOrDefault();
        }

        return ticketsCandidatos
            .OrderBy(t => t.DataCriacao)
            .FirstOrDefault();
    }
}
