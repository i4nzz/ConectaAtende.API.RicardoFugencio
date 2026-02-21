using ConectaAtende.Domain.Entidades;

namespace ConectaAtende.Application.Interfaces.InMemory;

public interface IOperationHistoryRepository
{
    void Registrar(OperacaoRealizada operacao);
    OperacaoRealizada? ObterUltima();
    void Limpar();
    int ObterQuantidadeOperacoes();
}

public record OperacaoRealizada(
    Guid Id,
    TipoOperacao Tipo,
    DateTime DataOperacao,
    Contato? ContatoAnterior,
    Contato? ContatoNovo
);

public enum TipoOperacao
{
    Criacao = 0,
    Atualizacao = 1,
    Exclusao = 2
}