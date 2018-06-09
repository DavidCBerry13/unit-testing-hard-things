using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Text;

namespace ReadingCsvFile.Testable
{
    public class StockQuoteProcessor
    {


        public StockQuoteProcessor(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }


        private IFileSystem _fileSystem;

        public void ProcessStockQuotes(String filename, String archiveDirectory)
        {
            this.CheckFileExists(filename);
            this.CheckFileHasData(filename);
            var stockQuoteData = this.ReadStockQuoteFile(filename);
            this.ProcessData(stockQuoteData);
            this.ArchiveFile(filename, archiveDirectory);
        }



        public void CheckFileExists(String filename)
        {
            if (!_fileSystem.File.Exists(filename))
                throw new FileNotFoundException($"File {filename} not found");
        }


        public void CheckFileHasData(String filename)
        {
            // FileInfo fileInfo = new FileInfo(filename);
            var fileInfo = _fileSystem.FileInfo.FromFileName(filename);
            if (fileInfo.Length == 0)
                throw new IOException($"File {filename} exists but it has zero bytes");
        }



        public List<StockQuote> ReadStockQuoteFile(String filename)
        {
            var stockQuotes = new List<StockQuote>();

            using (var reader = new StreamReader(_fileSystem.File.OpenRead(filename)))
            {
                CsvReader csvReader = new CsvReader(reader);
                csvReader.Configuration.HeaderValidated = ValidateHeader;
                csvReader.Configuration.RegisterClassMap<StockQuoteFileMap>();
                csvReader.Configuration.AllowComments = true;
                csvReader.Configuration.Comment = '#';
                csvReader.Configuration.IgnoreBlankLines = true;

                stockQuotes = csvReader.GetRecords<StockQuote>().ToList();
            }

            return stockQuotes;
        }

        public void ValidateHeader(bool isValid, string[] columnName, int columnIndex, ReadingContext context)
        {
            return;
        }


        public void ProcessData(List<StockQuote> stockQuoteData)
        {
            foreach (var quote in stockQuoteData)
            {
                var priceString = quote.ClosePrice?.ToString("c");
                var changeString = quote.Change?.ToString("c");

                Console.WriteLine($"{quote.Ticker,-5}   {quote.CompanyName,-50}    {priceString,-15}   {changeString,-10}");
            }
        }



        public String ArchiveFile(String filename, String archiveDirectory)
        {
            //if (!Directory.Exists(archiveDirectory))
            //    Directory.CreateDirectory(archiveDirectory);
            if (!_fileSystem.Directory.Exists(archiveDirectory))
                _fileSystem.Directory.CreateDirectory(archiveDirectory);


            String namePart = Path.GetFileNameWithoutExtension(filename);
            String extension = Path.GetExtension(filename);
            String timestamp = this.GetCurrentDateTime().ToString("yyyy-MM-dd_HH-mm-ss-fff");

            String archiveFilename = $"{namePart}.{timestamp}{extension}.archive";
            String archivePath = Path.Combine(archiveDirectory, archiveFilename);

            //File.Move(filename, archivePath);
            _fileSystem.File.Move(filename, archivePath);

            return archivePath;
        }


        // Needs to be virtual so we can mock the method with a partial mock
        public virtual DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }




    }
}
