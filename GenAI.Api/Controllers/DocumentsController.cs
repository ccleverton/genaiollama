using GenAI.Api.Application.Interfaces;
using GenAI.Api.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace GenAI.Api.Controllers;

[ApiController]
[Route("api/documents")]
public class DocumentsController : ControllerBase
{
    private readonly IDocumentIngestionService _service;

    public DocumentsController(IDocumentIngestionService service)
    {
        _service = service;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload(
        IFormFile file,
        [FromQuery] string system,
        [FromQuery] string source)
    {
        using var stream = file.OpenReadStream();

        await _service.ProcessPdf(stream, system, source);

        return Ok("Documento indexado com sucesso");
    }
}