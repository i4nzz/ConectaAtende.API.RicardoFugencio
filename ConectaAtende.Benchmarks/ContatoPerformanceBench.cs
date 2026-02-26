using BenchmarkDotNet.Attributes;
using ConectaAtende.Domain.Entidades;

namespace ConectaAtende.Benchmarks
{
    [MemoryDiagnoser]
    [RankColumn]     
    public class ContatoPerformanceBench
    {
        private List<Contato> _contatos;

        [Params(1000, 10000)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            _contatos = new List<Contato>();
            for (int i = 0; i < N; i++)
            {
                var c = new Contato(i.ToString().PadLeft(11, '0')) { Nome = $"Contato {i}" };
                _contatos.Add(c);
            }
        }

        [Benchmark(Description = "Inserção em Massa")]
        public List<Contato> InsercaoMassa()
        {
            var novaLista = new List<Contato>();
            for (int i = 0; i < N; i++)
            {
                novaLista.Add(new Contato("99999999999"));
            }
            return novaLista;
        }

        [Benchmark(Description = "Busca por Nome (LINQ)")]
        public Contato? BuscarPorNome() =>
            _contatos.FirstOrDefault(c => c.Nome == $"Contato {N - 1}");

        [Benchmark(Description = "Busca por Telefone/Número")]
        public Contato? BuscarPorTelefone() =>
            _contatos.FirstOrDefault(c => c.Numero == (N - 1).ToString().PadLeft(11, '0'));

        [Benchmark(Description = "Atualização em Massa")]
        public void AtualizarContatos()
        {
            foreach (var contato in _contatos)
            {
                contato.Nome += " - Atualizado";
                contato.DataAtualizacao = DateTime.Now;
            }
        }
    }
}