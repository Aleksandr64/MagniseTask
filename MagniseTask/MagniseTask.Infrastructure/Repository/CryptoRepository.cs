using System;
using System.Net.Http;
using System.Threading.Tasks;
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

        public async Task Test(string connectionId, string data)
        {
            // Construct the URL with query parameters
            var query = $"?connectionId={Uri.EscapeDataString(connectionId)}&data={Uri.EscapeDataString(data)}";
            var url = "/Crypto/Test" + query;

            // Send the POST request
            HttpResponseMessage response = await _httpClient.PostAsync(url, null);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Response received successfully:");
                Console.WriteLine(responseBody);
            }
            else
            {
                Console.WriteLine($"Request failed with status code: {response.StatusCode}");
            }
        }
    }
}