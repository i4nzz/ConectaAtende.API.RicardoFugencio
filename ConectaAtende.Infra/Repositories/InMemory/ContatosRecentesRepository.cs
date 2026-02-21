using ConectaAtende.Application.Interfaces.InMemory;
using ConectaAtende.Domain.Entidades;

namespace ConectaAtende.Infra.Repositories.InMemory;

public class ContatosRecentesRepository : IContatosRecentesRepository
{
    private static readonly List<ContatoRecente> _recentes = new();

    // Operacao in memory. Sem persistir em banco de dados.
    public Task RegistrarAcessoAsync(Guid contatoId)
    {
        var existente = _recentes.FirstOrDefault(x => x.ContatoId == contatoId);

        if (existente != null)
        {
            _recentes.Remove(existente);
        }

        _recentes.Insert(0, new ContatoRecente
        {
            Id = Guid.NewGuid(),
            ContatoId = contatoId,
            DataAcesso = DateTime.UtcNow
        });

        return Task.CompletedTask;
    }

    public Task<IEnumerable<ContatoRecente>> ObterRecentesAsync(int limite)
    {
        var resultado = _recentes
            .OrderByDescending(x => x.DataAcesso)
            .Take(limite);

        return Task.FromResult(resultado.AsEnumerable());
    }

    public Task RemoverAsync(Guid contatoId)
    {
        var existente = _recentes.FirstOrDefault(x => x.ContatoId == contatoId);

        if (existente != null)
        {
            _recentes.Remove(existente);
        }

        return Task.CompletedTask;
    }
}