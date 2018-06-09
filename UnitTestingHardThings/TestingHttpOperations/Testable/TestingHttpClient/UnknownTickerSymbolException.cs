using System;
using System.Collections.Generic;
using System.Text;

namespace TestingHttpClient
{
    public class UnknownTickerSymbolException : Exception
    {

        public UnknownTickerSymbolException(String ticker) 
            : base($"The ticker symbol {ticker} is unknown")
        {
            this.TickerSymbol = ticker;
        }


        public String TickerSymbol { get; private set; }

    }
}
