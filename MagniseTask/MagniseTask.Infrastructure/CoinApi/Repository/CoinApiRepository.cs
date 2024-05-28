using System.Net.WebSockets;
using System.Text;
using MagniseTask.Domain.Exceptions;
using MagniseTask.Infrastructure.CoinApi.ExternalEntity;
using MagniseTask.Infrastructure.CoinApi.Repository.Interface;
using MagniseTask.Infrastructure.CoinApi.Service;
using MagniseTask.Infrastructure.Repository.Interface;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace MagniseTask.Infrastructure.CoinApi.Repository
{
    public class CoinApiRepository : ICoinApiRepository
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly ICryptoRepository _cryptoRepository; 

        public CoinApiRepository(HttpClient httpClient, ICryptoRepository cryptoRepository, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _cryptoRepository = cryptoRepository; 
        }

        public async Task<IEnumerable<SymbolData>> GetSymbols(string filterSymbolId)
        {
            var requestUri = "v1/symbols";
            if (!string.IsNullOrEmpty(filterSymbolId))
            {
                requestUri += $"?filter_symbol_id={filterSymbolId}";
            }

            var response = await _httpClient.GetAsync(requestUri);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var symbols = JsonConvert.DeserializeObject<List<SymbolData>>(data);
                return symbols;
            }
            else
            {
                throw new ApiException(response.ReasonPhrase, response.StatusCode, response.Content);
            }
        }
        public async Task StartAsync(string connectionId, string[] message)
        {
            var ws = CoinApiWebSocketManager.GetSocket(connectionId);
            
            if (ws == null) 
            { 
                ws = new ClientWebSocket(); 
                await ws.ConnectAsync(new Uri(_configuration["CoinApi:WebSocketUrl"]!), CancellationToken.None);
                if (ws.State == WebSocketState.Open)
                {
                    CoinApiWebSocketManager.AddSocket(connectionId, ws);
                    _ = ReceiveMessagesAsync(ws, connectionId);
                }
            } 
            
            if (ws.State == WebSocketState.Open) 
            {
                var subscriptionMessage = new 
                { 
                    type = "hello", 
                    apikey = _configuration["CoinApi:MarketDataApiToken"], 
                    heartbeat = false, 
                    subscribe_data_type = new[] { "trade" }, 
                    subscribe_filter_symbol_id = message
                }; 
 
                string jsonMessage = JsonConvert.SerializeObject(subscriptionMessage);
                await SendMessageAsync(ws, jsonMessage); 
            } 
        } 
 
        private async Task ReceiveMessagesAsync(ClientWebSocket ws, string connectionId) 
        { 
            var buffer = new byte[1024 * 4]; 
 
            while (ws.State == WebSocketState.Open) 
            { 
                var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Text) 
                { 
                    var message = Encoding.UTF8.GetString(buffer, 0, result.Count); 
                    await _cryptoRepository.SendWebSocketMessage(connectionId, "ReceiveMessage",message); 
                } 
                else if (result.MessageType == WebSocketMessageType.Close)
                {
                    await StopAsync(connectionId);
                    if (result.CloseStatusDescription != null)
                    {
                        await _cryptoRepository.SendWebSocketMessage(connectionId, "Disconnect",result.CloseStatusDescription);
                    }
                } 
            } 
        } 
        
        public async Task StopAsync(string connectionId) 
        { 
            var ws = CoinApiWebSocketManager.GetSocket(connectionId); 
            if (ws != null && ws.State == WebSocketState.Open) 
            { 
                await ws.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                ws.Dispose(); 
                CoinApiWebSocketManager.RemoveSocket(connectionId);
            } 
        }
        
        private async Task SendMessageAsync(ClientWebSocket ws, string message) 
        { 
            var bytes = Encoding.UTF8.GetBytes(message); 
            await ws.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None); 
        } 
    }
}