using GenAI.Api.Configuration;
using GenAI.Api.Application.Services;
using GenAI.Api.Application.Interfaces;
using GenAI.Api.Infrastructure.Parsers;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Parsers
builder.Services.AddScoped<IPdfParser, PdfParser>();

// App Services (RAG pipeline)
builder.Services.AddScoped<IDocumentIngestionService, DocumentIngestionService>();
builder.Services.AddScoped<IChunkingService, ChunkingService>();


// Infrastructure
builder.Services.AddScoped<IElasticService, ElasticService>();

// Embeddings (Ollama via HttpClient)
builder.Services.AddHttpClient<IEmbeddingService, OllamaEmbeddingService>(client =>
{
    var baseUrl = builder.Configuration["Ollama:BaseUrl"];
    client.BaseAddress = new Uri(baseUrl);
});

builder.Services.AddScoped<ChatService>();

// Config extension (opcional, mas mantém padrão limpo)
builder.Services.AddGenAI();

var app = builder.Build();

// Pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();