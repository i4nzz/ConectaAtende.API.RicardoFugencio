using ConectaAtende.Domain.Entidades;

namespace ConectaAtende.Application.Interfaces.InMemory;

public interface IContatosRecentesRepository
{
    Task RegistrarAcessoAsync(Guid contatoId);
    Task<IEnumerable<ContatoRecente>> ObterRecentesAsync(int limite);
    Task RemoverAsync(Guid contatoId);
}
