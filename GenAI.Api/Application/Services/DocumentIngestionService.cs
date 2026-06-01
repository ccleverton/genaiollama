using GenAI.Api.Application.Interfaces;
using GenAI.Api.Domain.Models;
using GenAI.Api.Infrastructure.Elastic;
using GenAI.Api.Infrastructure.Parsers;

namespace GenAI.Api.Application.Services;

public class DocumentIngestionService : IDocumentIngestionService
{
    private readonly IPdfParser _pdfParser;
    private readonly IChunkingService _chunker;
    private readonly IEmbeddingService _embedder;
    private readonly IElasticService _elastic;

    public DocumentIngestionService(
        IPdfParser pdfParser,
        IChunkingService chunker,
        IEmbeddingService embedder,
        IElasticService elastic)
    {
        _pdfParser = pdfParser;
        _chunker = chunker;
        _embedder = embedder;
        _elastic = elastic;
    }

    public async Task ProcessPdf(Stream fileStream, string system, string source)
    {
        var text = _pdfParser.ExtractText(fileStream);

        var chunks = _chunker.Split(text);

        foreach (var chunk in chunks)
        {
            var embedding = await _embedder.GenerateAsync(chunk);

            var doc = new DocumentChunk
            {
                Content = chunk,
                System = system,
                Source = source,
                Embedding = embedding
            };

            await _elastic.IndexAsync(doc);
        }
    }
}