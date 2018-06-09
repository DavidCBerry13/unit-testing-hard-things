using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using Xunit;

namespace ReadingCsvFile.Testable.Tests
{
    public class StockQuoteProcessorTests
    {

        #region Test Data

        public const String VALID_DATA = @"symbol,companyName,open,close,high,low,latestVolume,previousClose,change,changePercent,week52High,week52Low
AAPL,Apple Inc.,188.27,188.58,,,17226115,188.15,0.43,0.00229,190.37,142.2
INTC,Intel Corporation,54.8,55.44,55.5,54.54,18707347,54.75,0.69,0.0126,55.79,33.23
MSFT,Microsoft Corporation,98.3,98.36,,,17944712,98.31,0.05,0.00051,98.98,68.02
FB,Facebook Inc.,186.16,184.92,,,10753879,185.93,-1.01,-0.00543,195.32,144.56
AMZN,Amazon.com Inc.,1603,1610.15,,,2644653,1603.07,7.08,0.00442,1638.1,927
GOOG,Alphabet Inc.,1079.02,1075.66,,,878687,1079.24,-3.58,-0.00332,1186.89,894.79
PS,Pluralsight Inc.,21.45,21.44,21.76,21,296676,21.25,0.19,0.00894,21.77,19.25
CRM,Salesforce.com Inc,128.73,127.96,129.25,127.62,3745234,128.74,-0.78,-0.00606,131,83.5501
IBM,International Business Machines Corporation,143.9,143.64,144.57,143.46,4902316,144.07,-0.43,-0.00298,171.13,139.13
ORCL,Oracle Corporation,46.54,47,47.165,46.48,10780191,46.46,0.54,0.01162,53.48,43.74";

        public const String VALID_DATA_WITH_BLANK_LINES_AT_END = @"symbol,companyName,open,close,high,low,latestVolume,previousClose,change,changePercent,week52High,week52Low
AAPL,Apple Inc.,188.27,188.58,,,17226115,188.15,0.43,0.00229,190.37,142.2
INTC,Intel Corporation,54.8,55.44,55.5,54.54,18707347,54.75,0.69,0.0126,55.79,33.23
MSFT,Microsoft Corporation,98.3,98.36,,,17944712,98.31,0.05,0.00051,98.98,68.02
FB,Facebook Inc.,186.16,184.92,,,10753879,185.93,-1.01,-0.00543,195.32,144.56
AMZN,Amazon.com Inc.,1603,1610.15,,,2644653,1603.07,7.08,0.00442,1638.1,927
GOOG,Alphabet Inc.,1079.02,1075.66,,,878687,1079.24,-3.58,-0.00332,1186.89,894.79
PS,Pluralsight Inc.,21.45,21.44,21.76,21,296676,21.25,0.19,0.00894,21.77,19.25
CRM,Salesforce.com Inc,128.73,127.96,129.25,127.62,3745234,128.74,-0.78,-0.00606,131,83.5501
IBM,International Business Machines Corporation,143.9,143.64,144.57,143.46,4902316,144.07,-0.43,-0.00298,171.13,139.13
ORCL,Oracle Corporation,46.54,47,47.165,46.48,10780191,46.46,0.54,0.01162,53.48,43.74


";


        public const String VALID_DATA_WITH_COMMENT_LINE = @"symbol,companyName,open,close,high,low,latestVolume,previousClose,change,changePercent,week52High,week52Low
# This is a comment and should not be treated as data
AAPL,Apple Inc.,188.27,188.58,,,17226115,188.15,0.43,0.00229,190.37,142.2
#INTC,Intel Corporation,54.8,55.44,55.5,54.54,18707347,54.75,0.69,0.0126,55.79,33.23
MSFT,Microsoft Corporation,98.3,98.36,,,17944712,98.31,0.05,0.00051,98.98,68.02
FB,Facebook Inc.,186.16,184.92,,,10753879,185.93,-1.01,-0.00543,195.32,144.56
AMZN,Amazon.com Inc.,1603,1610.15,,,2644653,1603.07,7.08,0.00442,1638.1,927
GOOG,Alphabet Inc.,1079.02,1075.66,,,878687,1079.24,-3.58,-0.00332,1186.89,894.79
PS,Pluralsight Inc.,21.45,21.44,21.76,21,296676,21.25,0.19,0.00894,21.77,19.25
CRM,Salesforce.com Inc,128.73,127.96,129.25,127.62,3745234,128.74,-0.78,-0.00606,131,83.5501
IBM,International Business Machines Corporation,143.9,143.64,144.57,143.46,4902316,144.07,-0.43,-0.00298,171.13,139.13
ORCL,Oracle Corporation,46.54,47,47.165,46.48,10780191,46.46,0.54,0.01162,53.48,43.74";


        public const String VALID_DATA_WITH_DUPLICATE_ROW = @"symbol,companyName,open,close,high,low,latestVolume,previousClose,change,changePercent,week52High,week52Low
# This is a comment and should not be treated as data
AAPL,Apple Inc.,188.27,188.58,,,17226115,188.15,0.43,0.00229,190.37,142.2
INTC,Intel Corporation,54.8,55.44,55.5,54.54,18707347,54.75,0.69,0.0126,55.79,33.23
MSFT,Microsoft Corporation,98.3,98.36,,,17944712,98.31,0.05,0.00051,98.98,68.02
FB,Facebook Inc.,186.16,184.92,,,10753879,185.93,-1.01,-0.00543,195.32,144.56
AMZN,Amazon.com Inc.,1603,1610.15,,,2644653,1603.07,7.08,0.00442,1638.1,927
GOOG,Alphabet Inc.,1079.02,1075.66,,,878687,1079.24,-3.58,-0.00332,1186.89,894.79
PS,Pluralsight Inc.,21.45,21.44,21.76,21,296676,21.25,0.19,0.00894,21.77,19.25
CRM,Salesforce.com Inc,128.73,127.96,129.25,127.62,3745234,128.74,-0.78,-0.00606,131,83.5501
IBM,International Business Machines Corporation,143.9,143.64,144.57,143.46,4902316,144.07,-0.43,-0.00298,171.13,139.13
ORCL,Oracle Corporation,46.54,47,47.165,46.48,10780191,46.46,0.54,0.01162,53.48,43.74
INTC,Intel Corporation,54.8,55.44,55.5,54.54,18707347,54.75,0.69,0.0126,55.79,33.23";

        #endregion


        [Fact]
        public void WhenFileDoesNotExist_FileNotFoundExceptionShouldBeThrown()
        {
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"c:\myfile.txt", new MockFileData("Testing is meh.") },
                { @"c:\demo\jQuery.js", new MockFileData("some js") },
                { @"c:\demo\image.gif", new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 }) }
            });

            StockQuoteProcessor sqp = new StockQuoteProcessor(fileSystem);

            Assert.Throws<FileNotFoundException>(() => sqp.CheckFileExists(@"c:\data\stock-data.csv"));

            
        }



        [Fact]
        public void WhenFileExist_NoExceptionShouldBeThrown()
        {
            // Arrange
            String datafileName = @"c:\data\stock-data.csv";
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { datafileName, new MockFileData("Testing is meh.") }
            });

            // Act
            StockQuoteProcessor sqp = new StockQuoteProcessor(fileSystem);
            sqp.CheckFileExists(@"c:\data\stock-data.csv");
        }



        [Fact]
        public void WhenFileExistWithZeroBytes_IOExceptionShouldBeThrown()
        {
            // Arrange
            String datafileName = @"c:\data\stock-data.csv";
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { datafileName, new MockFileData("") }
            });

            // Act
            StockQuoteProcessor sqp = new StockQuoteProcessor(fileSystem);
            Assert.Throws<IOException>(() => sqp.CheckFileHasData(@"c:\data\stock-data.csv"));
        }


        [Fact]
        public void WhenFileExistWithData_NoExceptionShouldBeThrown()
        {
            // Arrange
            String datafileName = @"c:\data\stock-data.csv";
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { datafileName, new MockFileData(VALID_DATA) }
            });

            // Act
            StockQuoteProcessor sqp = new StockQuoteProcessor(fileSystem);
            sqp.CheckFileHasData(@"c:\data\stock-data.csv");
        }




        [Fact]
        public void ArchiveProcessWillCreateArchiveDirectoryIfItDoesNotExist()
        {
            // Arrange
            String datafileName = @"c:\data\stock-data.csv";
            String archiveDirectory = @"C:\archive";
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { datafileName, new MockFileData(VALID_DATA) }
            });
            

            // Act
            StockQuoteProcessor sqp = new StockQuoteProcessor(fileSystem);
            sqp.ArchiveFile(@"c:\data\stock-data.csv", archiveDirectory);

            // Assert
            Assert.True(fileSystem.Directory.Exists(archiveDirectory));
        }



        [Fact]
        public void ArchiveProcessCorrectlyNamesArchiveFileWithDateStringAndArchiveExtension()
        {
            // Arrange
            DateTime dateTime = new DateTime(2018, 5, 30, 21, 30, 17);
            String datafileName = @"c:\data\stock-data.csv";
            String archiveDirectory = @"C:\archive";
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { datafileName, new MockFileData(VALID_DATA) }
            });
            fileSystem.Directory.CreateDirectory(archiveDirectory);

            var mock = new Mock<StockQuoteProcessor>(MockBehavior.Default, fileSystem);
            mock.CallBase = true;
            mock.Setup(x => x.GetCurrentDateTime()).Returns(dateTime);

            // Act
            mock.Object.ArchiveFile(@"c:\data\stock-data.csv", archiveDirectory);

            // Assert
            Assert.True(fileSystem.File.Exists("C:\\archive\\stock-data.2018-05-30_21-30-17-000.csv.archive"));
        }


        [Fact]
        public void ReadFileProcessIsAbleToHandleCommentsCorrectly()
        {
            // Arrange
            DateTime dateTime = new DateTime(2018, 5, 30, 21, 30, 17);
            String datafileName = @"c:\data\stock-data.csv";
            String archiveDirectory = @"C:\archive";
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { datafileName, new MockFileData(VALID_DATA_WITH_COMMENT_LINE) }
            });
            fileSystem.Directory.CreateDirectory(archiveDirectory);

            // Act
            StockQuoteProcessor sqp = new StockQuoteProcessor(fileSystem);
            var data = sqp.ReadStockQuoteFile(datafileName);

            // Assert
            Assert.Equal(9, data.Count);
            Assert.True(data.Count(x => x.Ticker == "INTC") == 0);   // Intel should be commented out
        }


    }
}
