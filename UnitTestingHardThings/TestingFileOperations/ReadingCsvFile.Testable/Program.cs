using System;
using System.IO.Abstractions;

namespace ReadingCsvFile.Testable
{
    class Program
    {
        static void Main(string[] args)
        {
            String filename = args[0];
            String archiveDirectory = args[1];

            IFileSystem fileSystem = new FileSystem();
            StockQuoteProcessor sqp = new StockQuoteProcessor(fileSystem);
            sqp.ProcessStockQuotes(filename, archiveDirectory);

            Console.ReadLine();
        }
    }
}
