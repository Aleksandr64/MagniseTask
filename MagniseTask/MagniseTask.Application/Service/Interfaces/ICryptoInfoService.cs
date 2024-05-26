using MagniseTask.Infrastructure.API.CoinApi.ExternalEntity;

namespace MagniseTask.Application.Service.Interfaces;

public interface ICryptoInfoService
{
    public Task<IEnumerable<Asset>> GetSupportedCryptoCurrencies();
}