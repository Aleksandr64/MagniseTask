using MagniseTask.Infrastructure.API.CoinApi.ExternalEntity;

namespace MagniseTask.Infrastructure.API.CoinApi.Interface;

public interface ICoinApiService
{
    public Task<IEnumerable<Asset>> GetAllAssets();
}