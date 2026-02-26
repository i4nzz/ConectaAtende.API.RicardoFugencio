using BenchmarkDotNet.Attributes;
using ConectaAtende.Domain.Entidades;

namespace ConectaAtende.Benchmarks
{
    [MemoryDiagnoser] // Mostra quanto de memória o código alocou
    public class ContatoBench
    {
        [Params(100, 1000)] // Testa com 100 e 1000 contatos
        public int N;

        [Benchmark]
        public List<Contato> CriarComFor()
        {
            var lista = new List<Contato>();
            for (int i = 0; i < N; i++)
            {
                lista.Add(new Contato(i.ToString()));
            }
            return lista;
        }

        [Benchmark]
        public List<Contato> CriarComLinq()
        {
            return Enumerable.Range(0, N)
                             .Select(i => new Contato(i.ToString()))
                             .ToList();
        }
    }
}