using Newtonsoft.Json;

namespace MagniseTask.Infrastructure.CoinApi.ExternalEntity;

public class SymbolData
{
    [JsonProperty("symbol_id")]
    public string SymbolId { get; set; }

    [JsonProperty("exchange_id")]
    public string ExchangeId { get; set; }

    [JsonProperty("symbol_type")]
    public string SymbolType { get; set; }

    [JsonProperty("asset_id_base")]
    public string AssetIdBase { get; set; }

    [JsonProperty("asset_id_quote")]
    public string AssetIdQuote { get; set; }

    [JsonProperty("data_start")]
    public DateTime DataStart { get; set; }

    [JsonProperty("data_end")]
    public DateTime DataEnd { get; set; }

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

    [JsonProperty("volume_1hrs")]
    public double Volume1Hrs { get; set; }

    [JsonProperty("volume_1hrs_usd")]
    public double Volume1HrsUsd { get; set; }

    [JsonProperty("volume_1day")]
    public double Volume1Day { get; set; }

    [JsonProperty("volume_1day_usd")]
    public double Volume1DayUsd { get; set; }

    [JsonProperty("volume_1mth")]
    public double Volume1Mth { get; set; }

    [JsonProperty("volume_1mth_usd")]
    public double Volume1MthUsd { get; set; }

    [JsonProperty("price")]
    public double Price { get; set; }

    [JsonProperty("symbol_id_exchange")]
    public string SymbolIdExchange { get; set; }

    [JsonProperty("price_precision")]
    public double PricePrecision { get; set; }

    [JsonProperty("size_precision")]
    public double SizePrecision { get; set; }
}
