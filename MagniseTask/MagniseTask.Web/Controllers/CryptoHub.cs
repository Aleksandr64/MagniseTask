using MagniseTask.Web.TempFile; 
using MagniseTask.Web.TempFile.Interface; 
using Microsoft.AspNetCore.SignalR; 
using WebSocketSharp; 
 
namespace MagniseTask.Web.Controllers; 
 
public class CryptoHub : Hub 
{ 
    private readonly ICoinApiClient _coinApiClient; 
 
    public CryptoHub(ICoinApiClient coinApiClient) 
    { 
        _coinApiClient = coinApiClient; 
    } 
     
    public async Task Send(string message) 
    { 
        _coinApiClient.Start(Context.ConnectionId ,message); 
    } 
     
    public async Task Disconnect() 
    { 
        var socket = CoinApiWebSocketManager.GetSocket(Context.ConnectionId); 
        if (socket.IsAlive) 
        { 
            socket.CloseAsync(); 
            Console.WriteLine("WebSocket connection closed."); 
            CoinApiWebSocketManager.RemoveSocket(Context.ConnectionId); 
        } 
        Context.Abort(); 
    } 
 
    public override async Task OnDisconnectedAsync(Exception? exception) 
    { 
        var socket = CoinApiWebSocketManager.GetSocket(Context.ConnectionId); 
        if (socket?.IsAlive ?? false) 
        { 
            socket.Close(); 
            Console.WriteLine("WebSocket connection closed."); 
            CoinApiWebSocketManager.RemoveSocket(Context.ConnectionId); 
        } 
        Context.Abort(); 
    } 
     
 
    private async Task SendMessageToClient(string connectionId, string message) 
    { 
        await Clients.Client(connectionId).SendAsync("ReceiveMessage", message); 
    } 
}