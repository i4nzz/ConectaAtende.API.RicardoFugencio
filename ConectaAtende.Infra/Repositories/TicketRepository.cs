using ConectaAtende.Domain.Entidades;
using ConectaAtende.Domain.Enums;
using ConectaAtende.Domain.Interface;
using ConectaAtende.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace ConectaAtende.Infra.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly AppDbContext _context;

    public TicketRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Ticket?> ObterPorIdAsync(Guid id)
    {
        return await _context.Tickets
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<Ticket>> ObterPorContatoAsync(Guid contatoId)
    {
        return await _context.Tickets
            .Where(t => t.ContatoId == contatoId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Ticket>> ObterPorStatusAsync(StatusTicketEnum status)
    {
        return await _context.Tickets
            .Where(t => t.Status == status)
            .ToListAsync();
    }

    public async Task<IEnumerable<Ticket>> ObterTodosAsync()
    {
        return await _context.Tickets.ToListAsync();
    }

    public async Task<Ticket> AdicionarAsync(Ticket ticket)
    {
        await _context.Tickets.AddAsync(ticket);
        await _context.SaveChangesAsync();
        return ticket;
    }

    public async Task<Ticket> AtualizarAsync(Ticket ticket)
    {
        _context.Tickets.Update(ticket);
        await _context.SaveChangesAsync();
        return ticket;
    }

    public async Task<bool> RemoverAsync(Guid id)
    {
        var ticket = await _context.Tickets.FindAsync(id);

        if (ticket is null)
            return false;

        _context.Tickets.Remove(ticket);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExisteAsync(Guid id)
    {
        return await _context.Tickets
            .AnyAsync(t => t.Id == id);
    }

    public async Task<Ticket?> ObterProximoDaFilaAsync()
    {
        return await _context.Tickets
            .Where(t => t.Status == StatusTicketEnum.NaFila)
            .OrderByDescending(t => t.Prioridade)
            .ThenBy(t => t.DataCriacao)
            .FirstOrDefaultAsync();
    }
}