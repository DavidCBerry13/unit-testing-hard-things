using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReadingCsvFile.NotTestable
{
    public class StockQuoteFileMap : ClassMap<StockQuote>
    {

        public StockQuoteFileMap()
        {

            Map(m => m.Ticker).Index(0);
            Map(m => m.CompanyName).Index(1);
            Map(m => m.OpenPrice).Index(2);
            Map(m => m.ClosePrice).Index(3);
            Map(m => m.DailyHigh).Index(4);
            Map(m => m.DailyLow).Index(5);
            Map(m => m.Volume).Index(6);
            Map(m => m.PreviousClose).Index(7);
            Map(m => m.Change).Index(8);
            Map(m => m.ChangePercent).Index(9);
            Map(m => m.Week52High).Index(10);
            Map(m => m.Week52Low).Index(11);

        }

    }
}
