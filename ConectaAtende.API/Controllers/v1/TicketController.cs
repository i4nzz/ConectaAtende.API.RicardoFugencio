using System.Net;
using ConectaAtende.Application.DTO.TicketDTO;
using ConectaAtende.Application.Interfaces;
using ConectaAtende.Application.Response;
using Microsoft.AspNetCore.Mvc;

namespace ConectaAtende.API.Controllers.v1;

[ApiController]
[Route("ConectaAtende/v1/Ticket/")]
public class TicketController : ControllerBase
{
    private readonly ITicketService _ticketService;

    public TicketController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    // POST ConectaAtende/v1/Ticket
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTicketRequest request)
    {
        var result = await _ticketService.CriarAsync(request);

        return StatusCode((int)HttpStatusCode.Created, new RespostaMetodos<object>
        {
            Sucesso = true,
            StatusCode = HttpStatusCode.Created,
            ObjetoRetorno = result.ObjetoRetorno
        });
    }

    //GET /tickets/next
    [HttpGet("next")]
    public async Task<IActionResult> GetNext()
    {
        var result = await _ticketService.ProximoAsync();
        
        if (!result.Sucesso)
        {
            return StatusCode((int)HttpStatusCode.NotFound, new RespostaMetodos<object>
            {
                Sucesso = false,
                StatusCode = HttpStatusCode.NotFound,
                MensagemErro = result.MensagemErro
            });
        }
            
        return StatusCode((int)HttpStatusCode.OK, new RespostaMetodos<object>
        {
            Sucesso = true,
            StatusCode = HttpStatusCode.OK,
            ObjetoRetorno = result.ObjetoRetorno
        });
    }


    [HttpPost("enqueue/{ticketID:guid}")]
    public async Task<IActionResult> Enqueue(Guid ticketID)
    {
        var result = await _ticketService.EnfileirarAsync(ticketID);

        if (!result.Sucesso)
        {
            return StatusCode((int)HttpStatusCode.NotFound, new RespostaMetodos<object>
            {
                Sucesso = false,
                StatusCode = HttpStatusCode.NotFound,
                MensagemErro = result.MensagemErro
            });
        }
        return StatusCode((int)HttpStatusCode.OK, new RespostaMetodos<object>
        {
            Sucesso = true,
            StatusCode = HttpStatusCode.OK,
            ObjetoRetorno = result.ObjetoRetorno
        });
    }

    [HttpPost("dequeue")]
    public async Task<IActionResult> Dequeue()
    {
        var nextResult = await _ticketService.ProximoAsync();

        if (!nextResult.Sucesso)
        {
            return StatusCode((int)HttpStatusCode.NotFound, new RespostaMetodos<object>
            {
                Sucesso = false,
                StatusCode = HttpStatusCode.NotFound,
                MensagemErro = nextResult.MensagemErro
            });
        }

        var ticket = nextResult.ObjetoRetorno;
        
        if (ticket == null)
        {
            return StatusCode((int)HttpStatusCode.NotFound, new RespostaMetodos<object>
            {
                Sucesso = false,
                StatusCode = HttpStatusCode.NotFound,
                MensagemErro = "Nenhum ticket na fila."
            });
        }
        
        var finalizeResult = await _ticketService.FinalizarAsync(ticket.Id);

        if (!finalizeResult.Sucesso)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new RespostaMetodos<object>
            {
                Sucesso = false,
                StatusCode = HttpStatusCode.InternalServerError,
                MensagemErro = finalizeResult.MensagemErro
            });
        }

        return StatusCode((int)HttpStatusCode.OK, new RespostaMetodos<object>
        {
            Sucesso = true,
            StatusCode = HttpStatusCode.OK,
            ObjetoRetorno = ticket
        });
    }

}
