using Newtonsoft.Json;

namespace MagniseTask.Infrastructure.API.CoinApi.ExternalEntity;

public class ChainAddress
{
    [JsonProperty("chain_id")]
    public string ChainId { get; set; }

    [JsonProperty("network_id")]
    public string NetworkId { get; set; }

    [JsonProperty("address")]
    public string Address { get; set; }
}