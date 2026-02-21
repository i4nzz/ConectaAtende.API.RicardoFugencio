using ConectaAtende.Application.DTO;
using ConectaAtende.Application.Interfaces;
using ConectaAtende.Application.Interfaces.InMemory;
using ConectaAtende.Application.Response;
using ConectaAtende.Domain.Entidades;
using ConectaAtende.Domain.Interface;

namespace ConectaAtende.Application.Services;

public class ContatoService : IContatoService
{
    private readonly IContatoRepository _contatoRepository;
    private readonly IOperationHistoryRepository _operationHistory;
    private readonly IContatosRecentesRepository _recentesRepository;

    public ContatoService(
        IContatoRepository contatoRepository,
        IOperationHistoryRepository operationHistory,
        IContatosRecentesRepository recentesRepository)
    {
        _contatoRepository = contatoRepository;
        _operationHistory = operationHistory;
        _recentesRepository = recentesRepository;
    }

    public async Task<RespostaMetodos<Contato>> CriarAsync(CreateContactRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Nome))
            return new RespostaMetodos<Contato>
            {
                Sucesso = false,
                MensagemErro = "Nome é obrigatório"
            };

        if (string.IsNullOrWhiteSpace(request.Telefone))
            return new RespostaMetodos<Contato>
            {
                Sucesso = false,
                MensagemErro = "Telefone é obrigatório"
            };

        var numeroNormalizado = new string(request.Telefone.Where(char.IsDigit).ToArray());

        var contato = new Contato(numeroNormalizado)
        {
            Id = Guid.NewGuid(),
            Nome = request.Nome.Trim(),
            DataCriacao = DateTime.UtcNow,
            DataAtualizacao = DateTime.UtcNow
        };

        var criado = await _contatoRepository.AdicionarAsync(contato);

        return new RespostaMetodos<Contato>
        {
            Sucesso = true,
            ObjetoRetorno = criado
        };
    }

    public async Task<RespostaMetodos<Contato?>> ObterPorIdAsync(Guid id)
    {
        var contato = await _contatoRepository.ObterPorIdAsync(id);

        if (contato is null)
            return new RespostaMetodos<Contato?>
            {
                Sucesso = false,
                MensagemErro = "Contato não encontrado"
            };

        await _recentesRepository.RegistrarAcessoAsync(contato.Id);

        return new RespostaMetodos<Contato?>
        {
            Sucesso = true,
            ObjetoRetorno = contato
        };
    }

    public async Task<RespostaMetodos<IEnumerable<Contato>>> ObterTodosAsync(int pagina, int tamanho)
    {
        var contatos = await _contatoRepository.ObterTodosAsync(pagina, tamanho);

        return new RespostaMetodos<IEnumerable<Contato>>
        {
            Sucesso = true,
            ObjetoRetorno = contatos
        };
    }

    public async Task<RespostaMetodos<bool>> AtualizarAsync(Guid id, UpdateContactRequest request)
    {
        var contato = await _contatoRepository.ObterPorIdAsync(id);

        if (contato is null)
            return new RespostaMetodos<bool>
            {
                Sucesso = false,
                MensagemErro = "Contato não encontrado"
            };

        if (!string.IsNullOrWhiteSpace(request.Nome))
            contato.Nome = request.Nome.Trim();

        if (!string.IsNullOrWhiteSpace(request.Telefone))
            contato.Numero = new string(request.Telefone.Where(char.IsDigit).ToArray());

        contato.DataAtualizacao = DateTime.UtcNow;

        await _contatoRepository.AtualizarAsync(contato);

        return new RespostaMetodos<bool>
        {
            Sucesso = true,
            ObjetoRetorno = true
        };
    }

    public async Task<RespostaMetodos<bool>> RemoverAsync(Guid id)
    {
        var contato = await _contatoRepository.ObterPorIdAsync(id);

        if (contato is null)
            return new RespostaMetodos<bool>
            {
                Sucesso = false,
                MensagemErro = "Contato não encontrado"
            };

        var removido = await _contatoRepository.RemoverAsync(id);

        return new RespostaMetodos<bool>
        {
            Sucesso = removido,
            ObjetoRetorno = removido
        };
    }

    public async Task<RespostaMetodos<IEnumerable<Contato>>> BuscarPorNomeAsync(string nome)
    {
        var resultado = await _contatoRepository.ObterPorNomeAsync(nome);

        return new RespostaMetodos<IEnumerable<Contato>>
        {
            Sucesso = true,
            ObjetoRetorno = resultado
        };
    }

    public async Task<RespostaMetodos<Contato?>> BuscarPorTelefoneAsync(string telefone)
    {
        var normalizado = new string(telefone.Where(char.IsDigit).ToArray());

        var contato = await _contatoRepository.ObterPorTelefoneAsync(normalizado);

        if (contato is null)
            return new RespostaMetodos<Contato?>
            {
                Sucesso = false,
                MensagemErro = "Contato não encontrado"
            };

        return new RespostaMetodos<Contato?>
        {
            Sucesso = true,
            ObjetoRetorno = contato
        };
    }

    public async Task<RespostaMetodos<bool>> DesfazerUltimaOperacaoAsync()
    {
        var ultima = _operationHistory.ObterUltima();

        if (ultima is null)
            return new RespostaMetodos<bool>
            {
                Sucesso = false,
                MensagemErro = "Nenhuma operação para desfazer"
            };

        switch (ultima.Tipo)
        {
            case TipoOperacao.Criacao:
                if (ultima.ContatoNovo != null)
                    await _contatoRepository.RemoverAsync(ultima.ContatoNovo.Id);
                break;

            case TipoOperacao.Atualizacao:
                if (ultima.ContatoAnterior != null)
                    await _contatoRepository.AtualizarAsync(ultima.ContatoAnterior);
                break;

            case TipoOperacao.Exclusao:
                if (ultima.ContatoAnterior != null)
                    await _contatoRepository.AdicionarAsync(ultima.ContatoAnterior);
                break;
        }

        return new RespostaMetodos<bool>
        {
            Sucesso = true,
            ObjetoRetorno = true
        };
    }

    public async Task<RespostaMetodos<IEnumerable<ContatoRecente>>> ObterRecentesAsync(int limite)
    {
        var recentes = await _recentesRepository.ObterRecentesAsync(limite);

        return new RespostaMetodos<IEnumerable<ContatoRecente>>
        {
            Sucesso = true,
            ObjetoRetorno = recentes
        };
    }
}