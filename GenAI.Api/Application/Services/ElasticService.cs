using Nest;
using GenAI.Api.Domain.Models;

namespace GenAI.Api.Application.Services;

public class ElasticService
{
    private readonly IElasticClient _client;

    public ElasticService()
    {
        var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
            .DefaultIndex("docs");

        _client = new ElasticClient(settings);
    }

    public async Task IndexAsync(DocumentChunk chunk)
    {
        await _client.IndexDocumentAsync(chunk);
    }
}