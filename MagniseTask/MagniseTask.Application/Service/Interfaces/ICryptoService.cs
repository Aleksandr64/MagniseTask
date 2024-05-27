using MagniseTask.Infrastructure.CoinApi.ExternalEntity;

namespace MagniseTask.Application.Service.Interfaces;

public interface ICryptoService
{
    public Task<IEnumerable<SymbolData>> GetCryptoPair(string filterSymbolId);
}