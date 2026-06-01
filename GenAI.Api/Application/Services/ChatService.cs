using GenAI.Api.Application.DTO;
using GenAI.Api.Application.Interfaces;
using GenAI.Api.Infrastructure.Elastic;
using System.Text;
using System.Text.Json;

namespace GenAI.Api.Application.Services;

public class ChatService
{
    private readonly IEmbeddingService _embedding;
    private readonly IElasticService _elastic;
    private readonly HttpClient _http;

    public ChatService(
        IEmbeddingService embedding,
        IElasticService elastic,
        HttpClient http)
    {
        _embedding = embedding;
        _elastic = elastic;
        _http = http;
    }

    public async Task<ChatResponse> AskAsync(ChatRequest request)
    {
        // 1. embedding da pergunta
        var questionEmbedding = await _embedding.GenerateAsync(request.Question);

        // 2. busca vetorial
        var chunks = await _elastic.SearchSimilarAsync(questionEmbedding, request.System);

        Console.WriteLine($"Chunks encontrados: {chunks.Count}");

        // 🔥 se não tiver contexto, evita chamar LLM inútil
        if (chunks.Count == 0)
        {
            return new ChatResponse
            {
                Answer = "Não encontrei informações relevantes nos documentos."
            };
        }

        // 3. monta contexto
        var context = string.Join("\n\n", chunks.Select(x => x.Content));

        Console.WriteLine("CONTEXT:");
        Console.WriteLine(context);

        // 4. prompt
        var prompt = $"""
Você é um assistente técnico.
Responda SOMENTE com base no contexto abaixo.

Se a resposta não estiver no contexto, diga que não encontrou informação.

CONTEXTO:
{context}

PERGUNTA:
{request.Question}
""";

        // 5. chamada correta do Ollama
        var requestBody = new
        {
            model = "qwen2.5:7b",
            prompt = prompt,
            stream = false
        };

        var httpResponse = await _http.PostAsync(
            "http://localhost:11434/api/generate",
            new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json"));

        var raw = await httpResponse.Content.ReadAsStringAsync();

        //Console.WriteLine("OLLAMA RAW RESPONSE:");
        //Console.WriteLine(raw);

        httpResponse.EnsureSuccessStatusCode();

        using var doc = JsonDocument.Parse(raw);

        var answer =
            doc.RootElement.TryGetProperty("response", out var responseProp)
                ? responseProp.GetString()
                : "Resposta não encontrada no modelo.";

        return new ChatResponse
        {
            Answer = answer
        };
    }
}