using GenAI.Api.Application.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

public class OllamaEmbeddingService : IEmbeddingService
{
    private readonly HttpClient _http;

    public OllamaEmbeddingService(HttpClient http)
    {
        _http = http;
    }

    public async Task<float[]> GenerateAsync(string text)
    {
        var response = await _http.PostAsJsonAsync("/api/embeddings", new
        {
            model = "nomic-embed-text",
            prompt = text
        });

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadFromJsonAsync<JsonElement>();

        if (json.TryGetProperty("embedding", out var embedding))
        {
            return embedding.EnumerateArray()
                .Select(x => x.GetSingle())
                .ToArray();
        }

        throw new Exception("Campo 'embedding' não encontrado na resposta do Ollama");
    }
}