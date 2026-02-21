using ConectaAtende.Application.Interfaces;
using ConectaAtende.Application.Interfaces.InMemory;
using ConectaAtende.Application.Policies;
using ConectaAtende.Application.Services;
using ConectaAtende.Domain.Interface;
using ConectaAtende.Infra.Repositories;
using ConectaAtende.Infra.Repositories.InMemory;

namespace ConectaAtende.API.IoC;

public static class ConfiguracaoInjecaoDeDependencia
{
    public static IServiceCollection ConfigurarDI(this IServiceCollection services)
    {
        
        // repository
        services.AddScoped<IContatoRepository, ContatoRepository>();
        services.AddScoped<ITicketRepository, TicketRepository>();

        // Repositorio em memory
        services.AddScoped<IOperationHistoryRepository, OperationHistoryRepository>();
        services.AddScoped<IContatosRecentesRepository, ContatosRecentesRepository>();

        services.AddScoped<ITriagePolicy, PrioridadeTriagePolicy>();
        
        // services
        services.AddScoped<IContatoService, ContatoService>();
        services.AddScoped<ITicketService, TicketService>();

        return services;
    }
}