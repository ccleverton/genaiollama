namespace GenAI.Api.Configuration;

public class AppSettings
{
    public ElasticSettings Elastic { get; set; }
    public OllamaSettings Ollama { get; set; }
}

public class ElasticSettings
{
    public string Url { get; set; }
    public string DefaultIndex { get; set; }
}

public class OllamaSettings
{
    public string BaseUrl { get; set; }
    public string EmbeddingModel { get; set; }
}