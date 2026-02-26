using Xunit;
using ConectaAtende.Domain.Entidades;

namespace ConectaAtende.Tests
{
    public class ContatoTests
    {
        [Fact]
        public void CriarContato_DeveInicializarComDataECelularCorretos()
        {
            string numeroEsperado = "11988887777";

            var contato = new Contato(numeroEsperado);

            Assert.Equal(numeroEsperado, contato.Numero);
            Assert.NotEqual(default, contato.DataCriacao);
            Assert.True(contato.DataCriacao <= DateTime.Now);
        }

        [Fact]
        public void Contato_DevePermitirSetarNomeManualmente()
        {
            var contato = new Contato("11988887777");
            var nomeEsperado = "Ricardo Fugencio";

            contato.Nome = nomeEsperado;

            Assert.Equal(nomeEsperado, contato.Nome);
        }
    }
}