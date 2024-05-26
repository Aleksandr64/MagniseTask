using System.Net.Http.Headers;
using MagniseTask.Domain.Exceptions;
using MagniseTask.Infrastructure.API.CoinApi.ExternalEntity;
using MagniseTask.Infrastructure.API.CoinApi.Interface;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace MagniseTask.Infrastructure.API.CoinApi;

public class CoinApiService : ICoinApiService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CoinApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IEnumerable<Asset>> GetAllAssets()
    {
        var client = _httpClientFactory.CreateClient();
        client.BaseAddress = new Uri("https://rest.coinapi.io/");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Add("X-CoinAPI-Key", Domain.Constants.CoinApi.MarketDataApiToken);

        HttpResponseMessage response = await client.GetAsync("v1/assets");

        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            var assets = JsonConvert.DeserializeObject<List<Asset>>(data);
            return assets;
        }
        else
        {
            throw new ApiException(response.ReasonPhrase, response.StatusCode, response.Content);
        }
    }
}