using System.Net;
using System.Net.Http.Headers;
using MagniseTask.Application.Service.Interfaces;
using MagniseTask.Domain.Constants;
using MagniseTask.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using WebSocketSharp;

namespace MagniseTask.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class CryptoController : Controller
{
    private readonly IHubContext<CryptoHub> _hubContext;
    private readonly ICryptoInfoService _cryptoInfoService;

    public CryptoController(IHubContext<CryptoHub> hubContext, ICryptoInfoService cryptoInfoService)
    {
        _hubContext = hubContext;
        _cryptoInfoService = cryptoInfoService;
    }

    [HttpGet("AssetsCrypto")]
    public async Task<IActionResult> GetAssetsCrypto()
    {
        var result = await _cryptoInfoService.GetSupportedCryptoCurrencies();
        return Ok(result);
    }

    [HttpPost("Test")]
    public async Task<IActionResult> SendWebSocketData(string connectionId, string data)
    {
        await _hubContext.Clients.Client(connectionId).SendAsync("ReceiveMessage", data);
        return Ok();
    }
}