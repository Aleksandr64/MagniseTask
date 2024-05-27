using MagniseTask.Infrastructure.CoinApi.Repository.Interface;
using Microsoft.AspNetCore.SignalR;

namespace MagniseTask.Web.Controllers
{
    public class CryptoHub : Hub
    {
        private readonly ICoinApiRepository _coinApiRepository;

        public CryptoHub(ICoinApiRepository coinApiRepository)
        {
            _coinApiRepository = coinApiRepository;
        }

        public async Task Send(string[] message)
        {
            await _coinApiRepository.StartAsync(Context.ConnectionId, message);
        }

        public async Task Disconnect()
        {
            await _coinApiRepository.StopAsync(Context.ConnectionId);
            Context.Abort();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await _coinApiRepository.StopAsync(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}