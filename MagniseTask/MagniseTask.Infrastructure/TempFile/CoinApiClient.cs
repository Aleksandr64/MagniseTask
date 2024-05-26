using MagniseTask.Domain.Constants; 
using MagniseTask.Infrastructure.Repository.Interface; 
using MagniseTask.Web.TempFile.Interface; 
using Newtonsoft.Json; 
using WebSocketSharp; 
 
namespace MagniseTask.Web.TempFile 
{ 
    public class CoinApiClient : ICoinApiClient 
    { 
        /*private readonly IHubContext<CryptoHub> _hubContext;*/ 
        private readonly ICryptoRepository _cryptoRepository; 
 
        public CoinApiClient(/*IHubContext<CryptoHub> hubContext,*/ ICryptoRepository cryptoRepository) 
        { 
            /*_hubContext = hubContext;*/ 
            _cryptoRepository = cryptoRepository; 
        } 
         
        public void Start(string connectionId, string message) 
        { 
            var ws = new WebSocket(CoinApi.CoinApiUrl); 
 
            ws.SslConfiguration.EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12; 
            ws.SslConfiguration.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true; 
 
            ws.OnMessage += async (sender, e) => 
            { 
                Console.WriteLine($"{e.Data}"); 
                await _cryptoRepository.Test(connectionId, e.Data); 
            }; 
 
            ws.OnError += (sender, e) => 
            { 
                Console.WriteLine($"Error: {e.Message}"); 
            }; 
 
            ws.OnClose += (sender, e) => 
            { 
                Console.WriteLine("WebSocket connection closed."); 
            }; 
             
            ws.Connect(); 
 
            if (ws.IsAlive) 
            { 
                CoinApiWebSocketManager.AddSocket(connectionId, ws); 
                var subscriptionMessage = new 
                { 
                    type = "hello", 
                    apikey = "45653FEB-D63A-427C-B5B9-DC88C35284AC", 
                    heartbeat = false, 
                    subscribe_data_type = new[] { "trade" }, 
                    subscribe_filter_symbol_id = new[] { 
                        "BITSTAMP_SPOT_BTC_USD$", 
                        "BITFINEX_SPOT_BTC_LTC$", 
                    } 
                }; 
 
                string jsonMessage = JsonConvert.SerializeObject(subscriptionMessage); 
                Console.WriteLine("Sending subscription message: " + jsonMessage); 
                ws.Send(jsonMessage); 
            } 
        } 
    } 
}