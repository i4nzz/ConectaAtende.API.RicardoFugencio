using ConectaAtende.Application.DTO.ContatoDTO;
using ConectaAtende.Application.Response;
using ConectaAtende.Domain.Entidades;

namespace ConectaAtende.Application.Interfaces;

public interface IContatoService
{
    Task<RespostaMetodos<Contato>> CriarAsync(CreateContactRequest request);
    Task<RespostaMetodos<Contato?>> ObterPorIdAsync(Guid id);
    Task<RespostaMetodos<IEnumerable<Contato>>> ObterTodosAsync(int pagina, int tamanho);
    Task<RespostaMetodos<bool>> AtualizarAsync(Guid id, UpdateContactRequest request);
    Task<RespostaMetodos<bool>> RemoverAsync(Guid id);
    Task<RespostaMetodos<IEnumerable<Contato>>> BuscarPorNomeAsync(string nome);
    Task<RespostaMetodos<Contato?>> BuscarPorTelefoneAsync(string telefone);

    Task<RespostaMetodos<bool>> DesfazerUltimaOperacaoAsync();
    Task<RespostaMetodos<IEnumerable<ContatoRecente>>> ObterRecentesAsync(int limite);
}