using ConectaAtende.Domain.Entidades;
using ConectaAtende.Domain.Enums;

namespace ConectaAtende.Domain.Interface;

public interface ITicketRepository
{
    Task<Ticket?> ObterPorIdAsync(Guid id);
    Task<IEnumerable<Ticket>> ObterPorContatoAsync(Guid contatoId);
    Task<IEnumerable<Ticket>> ObterPorStatusAsync(StatusTicketEnum status);
    Task<IEnumerable<Ticket>> ObterTodosAsync();
    Task<Ticket> AdicionarAsync(Ticket ticket);
    Task<Ticket> AtualizarAsync(Ticket ticket);
    Task<bool> RemoverAsync(Guid id);
    Task<bool> ExisteAsync(Guid id);
}