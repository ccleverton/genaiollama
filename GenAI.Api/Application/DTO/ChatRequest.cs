namespace GenAI.Api.Application.DTO;

public class ChatRequest
{
    public string Question { get; set; }
    public string System { get; set; }
}

public class ChatResponse
{
    public string Answer { get; set; }
}

public class OllamaResponse
{
    public string Response { get; set; }
}