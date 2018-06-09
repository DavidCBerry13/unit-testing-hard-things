using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TestingHttpClient
{
    public class StockQuoteClient
    {

        public StockQuoteClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        private HttpClient httpClient;




        private readonly String QUOTE_ACTION = "1.0/stock/{0}/quote";



        public async Task<StockQuote> GetStockQuote(String ticker)
        {
            String uriPath = String.Format(QUOTE_ACTION, ticker);
            try
            {
                var response = await httpClient.GetAsync(uriPath);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<StockQuote>();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new UnknownTickerSymbolException(ticker);
                }
                else
                {
                    throw new ApplicationException("Something went horribly wrong");
                }
            }
            catch (OperationCanceledException oce)
            {
                throw new TimeoutException("The operation timed out", oce);
            }
        }



    }
}
