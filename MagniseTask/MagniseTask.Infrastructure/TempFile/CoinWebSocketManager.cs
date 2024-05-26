using System.Collections.Concurrent; 
using WebSocketSharp; 
 
namespace MagniseTask.Web.TempFile; 
 
public static class CoinApiWebSocketManager 
{ 
    private static ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>(); 
 
    public static void AddSocket(string id, WebSocket socket) 
    { 
        _sockets.TryAdd(id, socket); 
    } 
 
    public static WebSocket GetSocket(string id) 
    { 
        _sockets.TryGetValue(id, out WebSocket webSocket); 
        return webSocket; 
    } 
 
    public static bool RemoveSocket(string id) 
    { 
        return _sockets.TryRemove(id, out _); 
    } 
}