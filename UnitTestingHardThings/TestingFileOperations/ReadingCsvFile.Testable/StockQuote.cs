using System;
using System.Collections.Generic;
using System.Text;

namespace ReadingCsvFile.Testable
{
    public class StockQuote
    {

        public String Ticker { get; set; }

        public String CompanyName { get; set; }

        public decimal? OpenPrice { get; set; }

        public decimal? ClosePrice { get; set; }

        public decimal? DailyHigh { get; set; }

        public decimal? DailyLow { get; set; }

        public long? Volume { get; set; }

        public decimal? PreviousClose { get; set; }

        public decimal? Change { get; set; }

        public decimal? ChangePercent { get; set; }

        public decimal? Week52High { get; set; }

        public decimal? Week52Low { get; set; }

    }
}
