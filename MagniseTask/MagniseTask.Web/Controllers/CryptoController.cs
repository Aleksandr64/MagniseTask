using MagniseTask.Application.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace MagniseTask.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class CryptoController : Controller
{
    private readonly IHubContext<CryptoHub> _hubContext;
    private readonly ICryptoService _cryptoService;

    public CryptoController(IHubContext<CryptoHub> hubContext, ICryptoService cryptoService)
    {
        _hubContext = hubContext;
        _cryptoService = cryptoService;
    }
    
    
    [HttpGet("CryptoPare")]
    public async Task<IActionResult> GetSymbols(string filterSymbolId)
    {
        var result = await _cryptoService.GetCryptoPair(filterSymbolId);
        return Ok(result);
    }

    [HttpPost("SendWebSocketMessage")]
    [SameServerOnly]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> SendWebSocketData(string connectionId, string data)
    {
        await _hubContext.Clients.Client(connectionId).SendAsync("ReceiveMessage", data);
        return Ok();
    }
}