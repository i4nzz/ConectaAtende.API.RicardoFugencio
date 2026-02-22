using ConectaAtende.Application.DTO.TicketDTO;
using ConectaAtende.Application.Response;
using ConectaAtende.Domain.Entidades;

namespace ConectaAtende.Application.Interfaces;

public interface ITicketService
{
    Task<RespostaMetodos<Ticket>> CriarAsync(CreateTicketRequest request);
    Task<RespostaMetodos<bool>> EnfileirarAsync(Guid ticketId);
    Task<RespostaMetodos<Ticket?>> ProximoAsync();
    Task<RespostaMetodos<bool>> FinalizarAsync(Guid ticketId);
}
