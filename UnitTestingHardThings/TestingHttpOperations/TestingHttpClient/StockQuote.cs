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



    //    {
    //  "symbol": "AAPL",
    //  "companyName": "Apple Inc.",
    //  "primaryExchange": "Nasdaq Global Select",
    //  "sector": "Technology",
    //  "calculationPrice": "tops",
    //  "open": 154,
    //  "openTime": 1506605400394,
    //  "close": 153.28,
    //  "closeTime": 1506605400394,
    //  "high": 154.80,
    //  "low": 153.25,
    //  "latestPrice": 158.73,
    //  "latestSource": "Previous close",
    //  "latestTime": "September 19, 2017",
    //  "latestUpdate": 1505779200000,
    //  "latestVolume": 20567140,
    //  "iexRealtimePrice": 158.71,
    //  "iexRealtimeSize": 100,
    //  "iexLastUpdated": 1505851198059,
    //  "delayedPrice": 158.71,
    //  "delayedPriceTime": 1505854782437,
    //  "previousClose": 158.73,
    //  "change": -1.67,
    //  "changePercent": -0.01158,
    //  "iexMarketPercent": 0.00948,
    //  "iexVolume": 82451,
    //  "avgTotalVolume": 29623234,
    //  "iexBidPrice": 153.01,
    //  "iexBidSize": 100,
    //  "iexAskPrice": 158.66,
    //  "iexAskSize": 100,
    //  "marketCap": 751627174400,
    //  "peRatio": 16.86,
    //  "week52High": 159.65,
    //  "week52Low": 93.63,
    //  "ytdChange": 0.3665,
    //}

}
