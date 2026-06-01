using GenAI.Api.Application.Interfaces;
using GenAI.Api.Domain.Models;
using Nest;

public class ElasticService : IElasticService
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

    public async Task<List<DocumentChunk>> SearchSimilarAsync(float[] embedding, string system)
    {
        var response = await _client.SearchAsync<DocumentChunk>(s => s
            .Index("docs")
            .Size(5)
            .Query(q => q
                .Bool(b => b
                    .Filter(f => f
                        .Term(t => t.Field("system").Value(system))
                    )
                    .Must(m => m
                        .ScriptScore(ss => ss
                            .Query(q => q.MatchAll())
                            .Script(script => script
                                .Source("cosineSimilarity(params.queryVector, 'embedding') + 1.0")
                                .Params(p => p.Add("queryVector", embedding))
                            )
                        )
                    )
                )
            )
        );

        return response.Documents.ToList();
    }
}