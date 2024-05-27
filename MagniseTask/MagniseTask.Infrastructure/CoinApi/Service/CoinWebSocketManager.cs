using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace MagniseTask.Infrastructure.CoinApi.Service 
{ 
    public static class CoinApiWebSocketManager 
    { 
        private static ConcurrentDictionary<string, ClientWebSocket> _sockets = new ConcurrentDictionary<string, ClientWebSocket>(); 
 
        public static void AddSocket(string id, ClientWebSocket socket) 
        { 
            _sockets.TryAdd(id, socket); 
        } 
 
        public static ClientWebSocket GetSocket(string id) 
        { 
            _sockets.TryGetValue(id, out ClientWebSocket webSocket); 
            return webSocket; 
        } 
 
        public static bool RemoveSocket(string id) 
        { 
            return _sockets.TryRemove(id, out _); 
        } 
    } 
}