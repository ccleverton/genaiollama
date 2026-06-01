//using System.Net.Http.Json;
//using GenAI.Api.Application.Interfaces;

//namespace GenAI.Api.Infrastructure.Embeddings;

//public class OllamaEmbeddingService : IEmbeddingService
//{
//    private readonly HttpClient _http;

//    public OllamaEmbeddingService(HttpClient http)
//    {
//        _http = http;
//    }

//    public async Task<float[]> GenerateAsync(string text)
//    {
//        var response = await _http.PostAsJsonAsync("/api/embeddings", new
//        {
//            model = "nomic-embed-text",
//            prompt = text
//        });

//        var json = await response.Content.ReadFromJsonAsync<EmbeddingResponse>();

//        return json?.Embedding ?? [];
//    }

//    private class EmbeddingResponse
//    {
//        public float[] Embedding { get; set; }
//    }
//}