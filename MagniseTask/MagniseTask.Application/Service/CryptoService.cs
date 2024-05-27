using System.Net;
using MagniseTask.Application.Service.Interfaces;
using MagniseTask.Domain.Exceptions;
using MagniseTask.Infrastructure.CoinApi.ExternalEntity;
using MagniseTask.Infrastructure.CoinApi.Repository.Interface;

namespace MagniseTask.Application.Service;

public class CryptoService : ICryptoService
{
    private readonly ICoinApiRepository _coinApiRepository;

    public CryptoService(ICoinApiRepository coinApiRepository)
    {
        _coinApiRepository = coinApiRepository;
    }
    
    public async Task<IEnumerable<SymbolData>> GetCryptoPair(string filterSymbolId)
    {
        var symbols = await _coinApiRepository.GetSymbols(filterSymbolId);
        
        if (symbols == null || !symbols.Any())
        {
            throw new ApiException("Error get data Crypto Currencies", HttpStatusCode.NotFound);
        }

        return symbols;
    }
}