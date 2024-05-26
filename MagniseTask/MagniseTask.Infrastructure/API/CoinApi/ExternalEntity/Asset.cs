using Newtonsoft.Json;

namespace MagniseTask.Infrastructure.API.CoinApi.ExternalEntity;

public class Asset
{
    [JsonProperty("asset_id")]
    public string AssetId { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("type_is_crypto")]
    public int TypeIsCrypto { get; set; }

    [JsonProperty("data_quote_start")]
    public DateTime DataQuoteStart { get; set; }

    [JsonProperty("data_quote_end")]
    public DateTime DataQuoteEnd { get; set; }

    [JsonProperty("data_orderbook_start")]
    public DateTime DataOrderbookStart { get; set; }

    [JsonProperty("data_orderbook_end")]
    public DateTime DataOrderbookEnd { get; set; }

    [JsonProperty("data_trade_start")]
    public DateTime DataTradeStart { get; set; }

    [JsonProperty("data_trade_end")]
    public DateTime DataTradeEnd { get; set; }

    [JsonProperty("data_symbols_count")]
    public int DataSymbolsCount { get; set; }

    [JsonProperty("volume_1hrs_usd")]
    public double Volume1HrsUsd { get; set; }

    [JsonProperty("volume_1day_usd")]
    public double Volume1DayUsd { get; set; }

    [JsonProperty("volume_1mth_usd")]
    public double Volume1MthUsd { get; set; }

    [JsonProperty("price_usd")]
    public double PriceUsd { get; set; }

    [JsonProperty("id_icon")]
    public string IdIcon { get; set; }

    [JsonProperty("supply_current")]
    public int SupplyCurrent { get; set; }

    [JsonProperty("supply_total")]
    public int SupplyTotal { get; set; }

    [JsonProperty("supply_max")]
    public int SupplyMax { get; set; }

    [JsonProperty("chain_addresses")]
    public List<ChainAddress> ChainAddresses { get; set; }

    [JsonProperty("data_start")]
    public string DataStart { get; set; }

    [JsonProperty("data_end")]
    public string DataEnd { get; set; }
}