using System.Net;

namespace ConectaAtende.Application.Response;

public class RespostaMetodos<T> 
{
    public bool Sucesso { get; set; }

    public T? ObjetoRetorno { get; set; }

    public HttpStatusCode? StatusCode { get; set; }

    public object? MensagemErro { get; set; }

    public static RespostaMetodos<T> SucessoResposta(T objetoRetorno, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new RespostaMetodos<T>
        {
            Sucesso = true,
            ObjetoRetorno = objetoRetorno,
            StatusCode = statusCode
        };
    }

    public static RespostaMetodos<T> ErroResposta(object mensagemErro, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new RespostaMetodos<T>
        {
            Sucesso = false,
            MensagemErro = mensagemErro,
            StatusCode = statusCode
        };
    }
}
