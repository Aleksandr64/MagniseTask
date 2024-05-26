using System.Net;
using MagniseTask.Application.Service.Interfaces;
using MagniseTask.Domain.Exceptions;
using MagniseTask.Infrastructure.API.CoinApi.ExternalEntity;
using MagniseTask.Infrastructure.API.CoinApi.Interface;

namespace MagniseTask.Application.Service;

public class CryptoInfoService : ICryptoInfoService
{
    private readonly ICoinApiService _coinApiService;

    public CryptoInfoService(ICoinApiService coinApiService)
    {
        _coinApiService = coinApiService;
    }

    public async Task<IEnumerable<Asset>> GetSupportedCryptoCurrencies()
    {
        var assets = await _coinApiService.GetAllAssets();

        if (assets == null || !assets.Any())
        {
            throw new ApiException("Error get data Crypto Currencies", HttpStatusCode.NotFound);
        }

        var endDateYesterday = DateTime.UtcNow.Date.AddDays(-1);
        var filteredAssets = assets
            .Where(item => item.TypeIsCrypto == 1) 
            .Where(item => DateTime.TryParse(item.DataEnd, out var endDate) &&
                   endDate >= endDateYesterday)
            .ToList();

        if (filteredAssets == null || !filteredAssets.Any())
        {
            throw new ApiException("Error filter Data", HttpStatusCode.BadRequest);
        }
        
        return filteredAssets;
    }
}