using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ReadingCsvFile.NotTestable
{
    public class StockQuoteProcessor
    {



        public void ProcessStockQuotes(String filename, String archiveDirectory)
        {
            this.CheckFileExists(filename);
            this.CheckFileHasData(filename);
            var stockQuoteData = this.ReadStockQuoteFile(filename);
            this.ProcessData(stockQuoteData);
            this.ArchiveFile(filename, archiveDirectory);
        }



        protected internal void CheckFileExists(String filename)
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException($"File {filename} not found");
        }


        protected internal void CheckFileHasData(String filename)
        {
            FileInfo fileInfo = new FileInfo(filename);
            if (fileInfo.Length == 0)
                throw new IOException($"File {filename} exists but it has zero bytes");
        }



        protected internal List<StockQuote> ReadStockQuoteFile(String filename)
        {
            var stockQuotes = new List<StockQuote>();

            using (var reader = new StreamReader(File.OpenRead(filename)))
            {
                CsvReader csvReader = new CsvReader(reader);
                csvReader.Configuration.HeaderValidated = ValidateHeader;
                csvReader.Configuration.RegisterClassMap<StockQuoteFileMap>();

                stockQuotes = csvReader.GetRecords<StockQuote>().ToList();
            }

            return stockQuotes;
        }

        public void ValidateHeader(bool isValid, string[] columnName, int columnIndex, ReadingContext context)
        {
            return;
        }


        protected internal void ProcessData(List<StockQuote> stockQuoteData)
        {
            foreach (var quote in stockQuoteData)
            {
                var priceString = quote.ClosePrice?.ToString("c");
                var changeString = quote.Change?.ToString("c");

                Console.WriteLine($"{quote.Ticker,-5}   {quote.CompanyName, -50}    {priceString, -15}   {changeString,-10}");
            }
        }



        protected internal void ArchiveFile(String filename, String archiveDirectory)
        {
            if (!Directory.Exists(archiveDirectory))
                Directory.CreateDirectory(archiveDirectory);


            String namePart = Path.GetFileNameWithoutExtension(filename);
            String extension = Path.GetExtension(filename);
            String timestamp = this.GetCurrentDateTime().ToString("yyyy-MM-dd_HH24-mm-ss-fff");
            
            String archiveFilename = $"{namePart}.{timestamp}.{extension}.archive";
            String archivePath = Path.Combine(archiveDirectory, archiveFilename);

            File.Move(filename, archivePath);
        }



        public DateTime GetCurrentDateTime()
        {           
            return DateTime.Now;
        }


    }
}
