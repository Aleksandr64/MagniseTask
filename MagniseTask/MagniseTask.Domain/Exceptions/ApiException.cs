using System.Net;

namespace MagniseTask.Domain.Exceptions;

public class ApiException : Exception
{
    public ApiException(string message, HttpStatusCode status, object data = default): base(message)
    {
        Data = data;
        Status = status;
    }
    
    public object Data { get; private set; }
    public HttpStatusCode Status { get; private set; }
}