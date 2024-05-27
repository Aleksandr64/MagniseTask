using System;
using System.Net.Http;
using System.Threading.Tasks;
using MagniseTask.Infrastructure.CoinApi.Repository.Interface;
using MagniseTask.Infrastructure.Repository.Interface;

namespace MagniseTask.Infrastructure.Repository
{
    public class CryptoRepository : ICryptoRepository
    {
        private readonly HttpClient _httpClient;
        
        public CryptoRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task SendWebSocketMessage(string connectionId, string data)
        {
            var query = $"?connectionId={Uri.EscapeDataString(connectionId)}&data={Uri.EscapeDataString(data)}";
            var url = "Crypto/SendWebSocketMessage" + query;

            await _httpClient.PostAsync(url, null);
        }
    }
}