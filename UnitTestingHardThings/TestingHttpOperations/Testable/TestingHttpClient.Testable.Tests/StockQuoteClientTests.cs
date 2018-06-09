using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace TestingHttpClient.Testable.Tests
{
    public class StockQuoteClientTests
    {


        public const String AAPL_QUOTE = @"{
symbol: ""AAPL"",
companyName: ""Apple Inc."",
primaryExchange: ""Nasdaq Global Select"",
sector: ""Technology"",
calculationPrice: ""close"",
open: 187.2,
openTime: 1526650200473,
close: 186.31,
closeTime: 1526673600412,
high: null,
low: null,
latestPrice: 186.31,
latestSource: ""Close"",
latestTime: ""May 18, 2018"",
latestUpdate: 1526673600412,
latestVolume: 18077917,
iexRealtimePrice: null,
iexRealtimeSize: null,
iexLastUpdated: null,
delayedPrice: 186.35,
delayedPriceTime: 1526677164231,
previousClose: 186.99,
change: -0.68,
changePercent: -0.00364,
iexMarketPercent: null,
iexVolume: null,
avgTotalVolume: 31994843,
iexBidPrice: null,
iexBidSize: null,
iexAskPrice: null,
iexAskSize: null,
marketCap: 915739360780,
peRatio: 19.15,
week52High: 190.37,
week52Low: 142.2,
ytdChange: 0.09016012147348036
}";



        [Fact]
        public async void TestStockTickerGoodResult()
        {
            // Arrange
            var httpClient = FakeHttpClientFactory.CreateFakeHttpClientForJsonStringContent(HttpStatusCode.OK, AAPL_QUOTE);
            StockQuoteClient client = new StockQuoteClient(httpClient);

            // Act
            var result = await client.GetStockQuote("AAPL");

            // Assert
            Assert.Equal("AAPL", result.Symbol);
        }


        [Fact]
        public async void WhenTickerSymbolNotFound_UnknownTickerSymbolExceptionThrown()
        {
            // Arrange

            var httpClient = FakeHttpClientFactory.CreateFakeHttpClientForStringContent(HttpStatusCode.NotFound, "text/plain", "Symbol not found");
            StockQuoteClient client = new StockQuoteClient(httpClient);

            // Act and Assert
            var result = await Assert.ThrowsAsync<UnknownTickerSymbolException>(() => client.GetStockQuote("ZVZZT") );

            Assert.Equal("ZVZZT", result.TickerSymbol);
        }


        [Fact]
        public async void WhenInternalServerError_UnknownTickerSymbolExceptionThrown()
        {
            // Arrange

            var httpClient = FakeHttpClientFactory.CreateFakeHttpClientForStringContent(HttpStatusCode.InternalServerError, "text/plain", "Internal Server Error");
            StockQuoteClient client = new StockQuoteClient(httpClient);

            // Act and Assert
            var result = await Assert.ThrowsAsync<ApplicationException>(() => client.GetStockQuote("ZVZZT"));

        }



        [Fact]
        public async void WhenServerTimeoutErrorOccurs_TimeoutExceptionThrown()
        {
            // Arrange

            var httpClient = FakeHttpClientFactory.CreateFakeHttpClientForTimeout();
            StockQuoteClient client = new StockQuoteClient(httpClient);

            // Act and Assert
            var result = await Assert.ThrowsAsync<TimeoutException>(() => client.GetStockQuote("ZVZZT"));
        }



        
    }
}
