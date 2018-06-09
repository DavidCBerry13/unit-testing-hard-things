using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace TestingHttpClient
{
    public class StockQuoteClient
    {

        public static readonly String BASE_ADDRESS = "https://api.iextrading.com/";

        public readonly String ACTION = "1.0/stock/{0}/quote";

        private static Lazy<HttpClient> iexTradingClient = new Lazy<HttpClient>(() =>
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.AutomaticDecompression = System.Net.DecompressionMethods.GZip;
            var connections = handler.MaxConnectionsPerServer;

            HttpClient httpClient = new HttpClient(handler);
            httpClient.BaseAddress = new Uri(BASE_ADDRESS);

            return httpClient;
        });
        
        public async Task<StockQuote> GetStockQuote(String ticker)
        {
            var httpClient = iexTradingClient.Value;

            String uriPath = String.Format(ACTION, ticker);
            var response = await httpClient.GetAsync(uriPath);

            if ( response.IsSuccessStatusCode )
            {
                return await response.Content.ReadAsAsync<StockQuote>();
            }
            else if ( response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new ApplicationException(message);
            }
            else
            {
                throw new ApplicationException("Something went horribly wrong");
            }           
        }



    }
}
