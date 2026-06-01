namespace GenAI.Api.Domain.Models;

public class DocumentChunk
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Content { get; set; }
    public string System { get; set; }
    public string Source { get; set; }
    public float[] Embedding { get; set; }
}