using ConectaAtende.Domain.Entidades;
using ConectaAtende.Domain.Interface;
using ConectaAtende.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace ConectaAtende.Infra.Repositories;

public class ContatoRepository : IContatoRepository
{
    private readonly AppDbContext _context;

    public ContatoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Contato?> ObterPorIdAsync(Guid id)
    {
        return await _context.Contatos
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Contato?> ObterPorTelefoneAsync(string numero)
    {
        var normalizado = new string(numero.Where(char.IsDigit).ToArray());

        return await _context.Contatos
            .FirstOrDefaultAsync(c => c.Numero == normalizado);
    }

    public async Task<IEnumerable<Contato>> ObterPorNomeAsync(string nome)
    {
        return await _context.Contatos
            .Where(c => c.Nome.Contains(nome))
            .ToListAsync();
    }

    public async Task<IEnumerable<Contato>> ObterTodosAsync(int pagina, int tamanho)
    {
        return await _context.Contatos
            .Skip((pagina - 1) * tamanho)
            .Take(tamanho)
            .ToListAsync();
    }

    public async Task<Contato> AdicionarAsync(Contato contato)
    {
        await _context.Contatos.AddAsync(contato);
        await _context.SaveChangesAsync();
        return contato;
    }

    public async Task<Contato> AtualizarAsync(Contato contato)
    {
        _context.Contatos.Update(contato);
        await _context.SaveChangesAsync();
        return contato;
    }

    public async Task<bool> RemoverAsync(Guid id)
    {
        var contato = await _context.Contatos.FindAsync(id);

        if (contato is null)
            return false;

        _context.Contatos.Remove(contato);
        await _context.SaveChangesAsync();

        return true;
    }
}