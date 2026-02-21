using ConectaAtende.Application.Interfaces.InMemory;

namespace ConectaAtende.Infra.Repositories.InMemory;

public class OperationHistoryRepository : IOperationHistoryRepository
{
    private static readonly List<OperacaoRealizada> _historico = new();

    public void Registrar(OperacaoRealizada operacao)
    {
        _historico.Add(operacao);
    }

    public OperacaoRealizada? ObterUltima()
    {
        return _historico.LastOrDefault();
    }

    public void Limpar()
    {
        _historico.Clear();
    }

    public int ObterQuantidadeOperacoes()
    {
        return _historico.Count;
    }
}