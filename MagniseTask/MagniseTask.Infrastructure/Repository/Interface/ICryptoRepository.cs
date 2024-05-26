namespace MagniseTask.Infrastructure.Repository.Interface;

public interface ICryptoRepository
{
    public Task Test(string connectionId, string data);
}