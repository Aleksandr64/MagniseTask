namespace MagniseTask.Infrastructure.Repository.Interface;

public interface ICryptoRepository
{
    public Task SendWebSocketMessage(string connectionId, string method, string data);
}