using GenAI.Api.Domain.Models;

namespace GenAI.Api.Application.Interfaces;

// PDF / TEXT
public interface IPdfParser
{
    string ExtractText(Stream fileStream);
    
}

// CHUNKING
public interface IChunkingService
{
    List<string> Split(string text, int size = 500);
}

// EMBEDDING
public interface IEmbeddingService
{
    Task<float[]> GenerateAsync(string text);
}

// ELASTIC
public interface IElasticService
{
    Task IndexAsync(DocumentChunk chunk);
    Task<List<DocumentChunk>> SearchSimilarAsync(float[] embedding, string system);
}

// PIPELINE PRINCIPAL
public interface IDocumentIngestionService
{
    Task ProcessPdf(Stream fileStream, string system, string source);
}