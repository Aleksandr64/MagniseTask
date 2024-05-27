using MagniseTask.Infrastructure.CoinApi.ExternalEntity;

namespace MagniseTask.Infrastructure.CoinApi.Repository.Interface;

public interface ICoinApiRepository
{
    public Task<IEnumerable<SymbolData>> GetSymbols(string filterSymbolId);
    public Task StartAsync(string connectionId, string[] message) ;
    Task StopAsync(string contextConnectionId);
}