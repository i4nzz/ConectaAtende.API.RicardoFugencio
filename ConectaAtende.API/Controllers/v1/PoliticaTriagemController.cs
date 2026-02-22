using System.Net;
using ConectaAtende.Application.Response;
using ConectaAtende.Application.Services;
using ConectaAtende.Domain.Enum;
using Microsoft.AspNetCore.Mvc;

namespace ConectaAtende.API.Controllers.v1;

[ApiController]
[Route("ConectaAtende/v1/PoliticaTriagem")]
public class PoliticaTriagemController : ControllerBase
{
    private readonly TriagePolicyState _policyState;

    public PoliticaTriagemController(TriagePolicyState policyState)
    {
        _policyState = policyState;
    }

    // GET /ConectaAtende/v1/PoliticaTriagem
    [HttpGet]
    public IActionResult Get()
    {
        return StatusCode((int)HttpStatusCode.OK, new RespostaMetodos<object>
        {
            Sucesso = true,
            StatusCode = HttpStatusCode.OK,
            ObjetoRetorno = new
            {
                Current = _policyState.Current.ToString(),
                Available = Enum.GetNames(typeof(TriagePolicyType))
            }
        });
    }

    public record SetPolicyRequest(string Policy);

    // POST /ConectaAtende/v1/PoliticaTriagem
    [HttpPost]
    public IActionResult Set([FromBody] SetPolicyRequest request)
    {
        if (request is null || string.IsNullOrWhiteSpace(request.Policy))
        {
            return StatusCode((int)HttpStatusCode.BadRequest, new RespostaMetodos<object>
            {
                Sucesso = false,
                StatusCode = HttpStatusCode.BadRequest,
                MensagemErro = "Policy é obrigatório."
            });
        }


        if (!Enum.TryParse<TriagePolicyType>(request.Policy, true, out var parsed))
        {
            return StatusCode((int)HttpStatusCode.BadRequest, new RespostaMetodos<object>
            {
                Sucesso = false,
                StatusCode = HttpStatusCode.BadRequest,
                MensagemErro = $"Política desconhecida: {request.Policy}"
            });
        }

        _policyState.Current = parsed;

        return StatusCode((int)HttpStatusCode.OK, new RespostaMetodos<object>
        {
            Sucesso = true,
            StatusCode = HttpStatusCode.OK,
            ObjetoRetorno = new { Current = _policyState.Current.ToString() }
        });
    }
}
