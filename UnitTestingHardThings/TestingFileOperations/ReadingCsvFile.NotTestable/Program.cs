using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;

namespace ReadingCsvFile.NotTestable
{
    class Program
    {
        static void Main(string[] args)
        {
            String filename = args[0];
            String archiveDirectory = args[1];

            StockQuoteProcessor sqp = new StockQuoteProcessor();
            sqp.ProcessStockQuotes(filename, archiveDirectory);

            Console.ReadLine();
        }






    }
}
