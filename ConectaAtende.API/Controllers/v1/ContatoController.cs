using System.Net;
using ConectaAtende.Application.DTO.ContatoDTO;
using ConectaAtende.Application.Interfaces;
using ConectaAtende.Application.Response;
using Microsoft.AspNetCore.Mvc;

namespace ConectaAtende.API.Controllers.v1;

[ApiController]
[Route("ConectaAtende/v1/Contato")]
public class ContatoController : ControllerBase
{
    private readonly IContatoService _contatoService;

    public ContatoController(IContatoService contatoService)
    {
        _contatoService = contatoService;
    }

    // POST ConectaAtende/v1/Contato
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateContactRequest request)
    {
        var result = await _contatoService.CriarAsync(request);

        return StatusCode((int)HttpStatusCode.Created, new RespostaMetodos<object>
        {
            Sucesso = true,
            StatusCode = HttpStatusCode.Created,
            ObjetoRetorno = result.ObjetoRetorno
        });
    }

    // GET ConectaAtende/v1/Contato/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var contato = await _contatoService.ObterPorIdAsync(id);

        if (contato is null) return NotFound();

        return StatusCode((int)HttpStatusCode.OK, new RespostaMetodos<object>
        {
            Sucesso = true,
            StatusCode = HttpStatusCode.OK,
            ObjetoRetorno = contato.ObjetoRetorno
        });
    }

    // PUT ConectaAtende/v1/Contato/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateContactRequest request)
    {
        var result = await _contatoService.AtualizarAsync(id, request);

        if (!result.Sucesso)
            return StatusCode((int)HttpStatusCode.NotFound, new RespostaMetodos<object>
            {
                Sucesso = false,
                StatusCode = HttpStatusCode.NotFound,
                MensagemErro = result.MensagemErro,
                ObjetoRetorno = null
            });

        return StatusCode((int)HttpStatusCode.OK, new RespostaMetodos<object>
        {
            Sucesso = true,
            StatusCode = HttpStatusCode.OK,
            ObjetoRetorno = result.ObjetoRetorno
        });
    }

    // DELETE ConectaAtende/v1/Contato/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var resposta = await _contatoService.RemoverAsync(id);

        if (!resposta.Sucesso)
        {
            return StatusCode((int)HttpStatusCode.NotFound, new RespostaMetodos<object>
            {
                Sucesso = false,
                StatusCode = HttpStatusCode.NotFound,
                MensagemErro = resposta.MensagemErro,
                ObjetoRetorno = null
            });
        }

        return StatusCode((int)HttpStatusCode.OK, new RespostaMetodos<object>
        {
            Sucesso = true,
            StatusCode = HttpStatusCode.OK,
            ObjetoRetorno = null
        });
    }

    // GET ConectaAtende/v1/Contato?page=&pageSize=
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var result = await _contatoService.ObterTodosAsync(page, pageSize);
        return StatusCode((int)HttpStatusCode.OK, new RespostaMetodos<object>
        {
            Sucesso = true,
            StatusCode = HttpStatusCode.OK,
            ObjetoRetorno = result.ObjetoRetorno
        });
    }

    // GET ConectaAtende/v1/Contato/search?name= ou ?phone=
    [HttpGet("search")]
    public async Task<IActionResult> Search(
        [FromQuery] string? name,
        [FromQuery] string? phone)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return StatusCode((int)HttpStatusCode.BadRequest, new RespostaMetodos<object>
            {
                Sucesso = false,
                StatusCode = HttpStatusCode.BadRequest,
                MensagemErro = "Nome não pode ser vazio",
                ObjetoRetorno = null
            });
        }
            

        if (string.IsNullOrWhiteSpace(phone))
        {
            return StatusCode((int)HttpStatusCode.BadRequest, new RespostaMetodos<object>
            {
                Sucesso = false,
                StatusCode = HttpStatusCode.BadRequest,
                MensagemErro = "Telefone não pode ser vazio",
                ObjetoRetorno = null
            });
        }


        return StatusCode((int)HttpStatusCode.OK, new RespostaMetodos<object>
        {
            Sucesso = true,
            StatusCode = HttpStatusCode.OK,
            ObjetoRetorno = !string.IsNullOrWhiteSpace(name) ? await _contatoService.BuscarPorNomeAsync(name) : await _contatoService.BuscarPorTelefoneAsync(phone!)
        });
    }

    // POST ConectaAtende/v1/Contato/undo
    [HttpPost("undo")]
    public async Task<IActionResult> Undo()
    {
        var resposta = await _contatoService.DesfazerUltimaOperacaoAsync();

        if (!resposta.Sucesso)
        {
            return StatusCode((int)HttpStatusCode.BadRequest, new RespostaMetodos<object>
            {
                Sucesso = false,
                StatusCode = HttpStatusCode.BadRequest,
                MensagemErro = resposta.MensagemErro,
                ObjetoRetorno = null
            });
        }

        return StatusCode((int)HttpStatusCode.OK, new RespostaMetodos<object>
        {
            Sucesso = true,
            StatusCode = HttpStatusCode.OK,
            ObjetoRetorno = resposta.ObjetoRetorno
        });
    }

    // GET ConectaAtende/v1/Contato/recent?limit=
    [HttpGet("recent")]
    public async Task<IActionResult> GetRecent([FromQuery] int limit = 5)
    {
        var result = await _contatoService.ObterRecentesAsync(limit);
        return StatusCode((int)HttpStatusCode.OK, new RespostaMetodos<object>
        {
            Sucesso = true,
            StatusCode = HttpStatusCode.OK,
            ObjetoRetorno = result.ObjetoRetorno
        });
    }
}