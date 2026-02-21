using ConectaAtende.Domain.Entidades;

namespace ConectaAtende.Domain.Interface;

public interface IContatoRepository
{
    Task<Contato?> ObterPorIdAsync(Guid id);
    Task<Contato?> ObterPorTelefoneAsync(string numero);
    Task<IEnumerable<Contato>> ObterPorNomeAsync(string nome);
    Task<IEnumerable<Contato>> ObterTodosAsync(int pagina, int tamanho);
    Task<Contato> AdicionarAsync(Contato contato);
    Task<Contato> AtualizarAsync(Contato contato);
    Task<bool> RemoverAsync(Guid id);
}