using System;
using System.Threading.Tasks;

namespace TestingHttpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Ticker Symbol:");
            var ticker = Console.ReadLine();


            StockQuoteClient client = new StockQuoteClient();
            try
            {
                var quoteTask = client.GetStockQuote(ticker);
                Task.WaitAll(quoteTask);
                PrintQuote(quoteTask.Result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An Error Occured: {ex.Message}");
            }

            Console.ReadKey();
        }


        static void PrintQuote(StockQuote quote)
        {
            Console.WriteLine($"Ticker:           {quote.Symbol}");
            Console.WriteLine($"Company:          {quote.CompanyName}");
            Console.WriteLine($"Closing Price:    {quote.Close}");
            Console.WriteLine($"Change:           {quote.Change}");
            Console.WriteLine($"Change Percent:   {quote.ChangePercent}");
            Console.WriteLine($"High:             {quote.High}");
            Console.WriteLine($"Low:              {quote.Low}");
        }

    }
}
