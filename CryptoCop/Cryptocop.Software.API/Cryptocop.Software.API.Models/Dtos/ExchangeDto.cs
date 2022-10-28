using Newtonsoft.Json;
using System;

namespace Cryptocop.Software.API.Models.Dtos
{
    public class ExchangeDto
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "exchange_name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "exchange_slug")]
        public string Slug { get; set; }

        [JsonProperty(PropertyName = "base_asset_symbol")]
        public string AssetSymbol { get; set; }

        [JsonProperty(PropertyName = "price_usd")]
        public double? PriceInUsd { get; set; }

        [JsonProperty(PropertyName = "last_trade_at")]
        public DateTime? LastTrade { get; set; }
        
    }
}