using System.Net;
using ConectaAtende.Application.DTO.TicketDTO;
using ConectaAtende.Application.Interfaces;
using ConectaAtende.Application.Policies;
using ConectaAtende.Application.Response;
using ConectaAtende.Domain.Entidades;
using ConectaAtende.Domain.Enum;
using ConectaAtende.Domain.Enums;
using ConectaAtende.Domain.Interface;
using ConectaAtende.Domain.Interfaces;

namespace ConectaAtende.Application.Services;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IContatoRepository _contatoRepository;

    private readonly ITriagePolicy _ordemPolicy;
    private readonly ITriagePolicy _prioridadePolicy;
    private readonly ITriagePolicy _mistaPolicy;
    private readonly TriagePolicyState _policyState;

    public TicketService(
        ITicketRepository ticketRepository,
        IContatoRepository contatoRepository,
        OrdemChegadaTriagePolicy ordemPolicy,
        PrioridadeTriagePolicy prioridadePolicy,
        MistaTriagePolicy mistaPolicy,
        TriagePolicyState policyState)
    {
        _ticketRepository = ticketRepository;
        _contatoRepository = contatoRepository;
        _ordemPolicy = ordemPolicy;
        _prioridadePolicy = prioridadePolicy;
        _mistaPolicy = mistaPolicy;
        _policyState = policyState;
    }

    public async Task<RespostaMetodos<Ticket>> CriarAsync(CreateTicketRequest request)
    {
        var contato = await _contatoRepository.ObterPorIdAsync(request.ContatoId);

        if (contato == null)
        {
            return new RespostaMetodos<Ticket>
            {
                Sucesso = false,
                StatusCode = HttpStatusCode.BadRequest,
                MensagemErro = "Contato inválido."
            };
        }

        var ticket = new Ticket
        {
            Id = Guid.NewGuid(),
            ContatoId = request.ContatoId,
            Descricao = request.Descricao,
            Prioridade = request.Prioridade,
            Status = StatusTicketEnum.Criado,
            DataCriacao = DateTime.UtcNow
        };

        var ticketAdc = await _ticketRepository.AdicionarAsync(ticket);

        return new RespostaMetodos<Ticket>
        {
            Sucesso = true,
            StatusCode = HttpStatusCode.Created,
            ObjetoRetorno = ticket
        };
    }

    public async Task<RespostaMetodos<bool>> EnfileirarAsync(Guid ticketId)
    {
        var ticket = await _ticketRepository.ObterPorIdAsync(ticketId);

        if (ticket == null)
        {
            return new RespostaMetodos<bool>
            {
                Sucesso = false,
                MensagemErro = "Ticket não encontrado.",
                StatusCode = HttpStatusCode.NotFound
            };
        }
         

        if (ticket.Status != StatusTicketEnum.Criado)
        {
            return new RespostaMetodos<bool>
            {
                Sucesso = false,
                MensagemErro = "Ticket deve estar no status 'Criado' para ser enfileirado.",
                StatusCode = HttpStatusCode.BadRequest
            };
        }

        ticket.Status = StatusTicketEnum.NaFila;
        ticket.DataAlteracao = DateTime.UtcNow;

        var objAtualizado = await _ticketRepository.AtualizarAsync(ticket);
        
        return new RespostaMetodos<bool>
        {
            Sucesso = true
        };
    }

    public async Task<RespostaMetodos<Ticket?>> ProximoAsync()
    {
        var ticketsNaFila = await _ticketRepository
            .ObterPorStatusAsync(StatusTicketEnum.NaFila);

        
        ITriagePolicy policy = _policyState.Current switch
        {
            TriagePolicyType.Prioridade => _prioridadePolicy,
            TriagePolicyType.Mista => _mistaPolicy,
            _ => _ordemPolicy
        };

        var proximo = policy.SelecionarProximo(ticketsNaFila);

        if (proximo == null)
        {
            return new RespostaMetodos<Ticket?>
            {
                Sucesso = false,
                MensagemErro = "Nenhum ticket na fila.",
                ObjetoRetorno = null,
            };
        }

        proximo.Status = StatusTicketEnum.EmAtendimento;
        proximo.DataAlteracao = DateTime.UtcNow;

        var atualizado = await _ticketRepository.AtualizarAsync(proximo);

        return new RespostaMetodos<Ticket?>
        {
            Sucesso = true,
            ObjetoRetorno = atualizado
        };
    }

    public async Task<RespostaMetodos<bool>> FinalizarAsync(Guid ticketId)
    {
        var ticket = await _ticketRepository.ObterPorIdAsync(ticketId);

        if (ticket == null)
        {
            return new RespostaMetodos<bool>
            {
                Sucesso = false,
                MensagemErro = "Ticket não encontrado.",
            };
        }
            

        if (ticket.Status != StatusTicketEnum.EmAtendimento)
        {
            return new RespostaMetodos<bool>
            {
                Sucesso = false,
                MensagemErro = "Ticket deve estar no status 'Em Atendimento' para ser finalizado.",
            };
        }

        ticket.Status = StatusTicketEnum.Finalizado;
        ticket.DataAlteracao = DateTime.UtcNow;

        var atualizado = await _ticketRepository.AtualizarAsync(ticket);

        return new RespostaMetodos<bool>
        {
            Sucesso = true
        };
    }
}
