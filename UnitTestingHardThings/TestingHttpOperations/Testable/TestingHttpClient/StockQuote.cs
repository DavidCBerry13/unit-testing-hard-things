using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestingHttpClient
{
    public class StockQuote
    {
        [JsonProperty("symbol")]
        public String Symbol { get; set; }

        [JsonProperty("companyName")]
        public String CompanyName { get; set; }

        [JsonProperty("primaryExchange")]
        public String PrimaryExchange { get; set; }

        [JsonProperty("sector")]
        public String Sector { get; set; }

        [JsonProperty("calculationPrice")]
        public String CalculationPrice { get; set; }

        [JsonProperty("open")]
        public decimal? Open { get; set; }

        [JsonProperty("openTime")]
        public long? OpenTime { get; set; }

        [JsonProperty("close")]
        public decimal? Close { get; set; }

        [JsonProperty("closeTime")]
        public long? CloseTime { get; set; }

        [JsonProperty("high")]
        public decimal? High { get; set; }

        [JsonProperty("low")]
        public decimal? Low { get; set; }

        [JsonProperty("previousClose")]
        public decimal? PreviousClose { get; set; }

        [JsonProperty("change")]
        public decimal? Change { get; set; }

        [JsonProperty("changePercent")]
        public decimal? ChangePercent { get; set; }

        [JsonProperty("latestVolume")]
        public long? Volume { get; set; }

    }
}
