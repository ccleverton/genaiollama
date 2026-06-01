using GenAI.Api.Application.DTO;
using GenAI.Api.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace GenAI.Api.Controllers;

[ApiController]
[Route("api/chat")]
public class ChatController : ControllerBase
{
    private readonly ChatService _service;

    public ChatController(ChatService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Ask(ChatRequest request)
    {
        var result = await _service.AskAsync(request);
        return Ok(result);
    }
}