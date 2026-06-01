using GenAI.Api.Application.Services;
using GenAI.Api.Application.Interfaces;
using GenAI.Api.Infrastructure.Elastic;
using GenAI.Api.Infrastructure.Parsers;

namespace GenAI.Api.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGenAI(this IServiceCollection services)
    {
        // Parsers
        services.AddScoped<IPdfParser, PdfParser>();

        // Services (Application)
        services.AddScoped<IChunkingService, ChunkingService>();
        services.AddScoped<IDocumentIngestionService, DocumentIngestionService>();

        // Infrastructure
        services.AddScoped<IElasticService, ElasticService>();

        // Embedding via HttpClient (Ollama)
        services.AddHttpClient<OllamaEmbeddingService>();

        return services;
    }
}