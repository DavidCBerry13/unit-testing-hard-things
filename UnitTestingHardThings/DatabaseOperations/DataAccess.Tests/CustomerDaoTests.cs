using Moq;
using System;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace DataAccess.Tests
{
    public class CustomerDaoTests
    {

        [Fact]
        public void TestGetCustomersWorks()
        {
            // Arrange
            Mock<IDataReader> readerMock = new Mock<IDataReader>();
            readerMock.Setup(r => r.Read()).ReturnsInOrder(true, true, true, false);
            readerMock.Setup(r => r.GetInt32(0)).ReturnsInOrder(1, 2, 3);
            readerMock.Setup(r => r.GetString(1)).ReturnsInOrder("John", "Jane", "Bob");
            readerMock.Setup(r => r.GetString(2)).ReturnsInOrder("Smith", "Carter", "Adams");
            readerMock.Setup(r => r.GetString(3)).ReturnsInOrder("jsmith@xyz.com", "jcarter@abc.com", "badams@asdf.com");
            Mock<IDbCommand> mockCommand = new Mock<IDbCommand>();
            mockCommand.Setup(c => c.ExecuteReader()).Returns(readerMock.Object);
            Mock<IDbConnection> connectionMock = new Mock<IDbConnection>();
            connectionMock.Setup(c => c.CreateCommand()).Returns(mockCommand.Object);

            Mock<CustomerDao> dao = new Mock<CustomerDao>("Server=myServerAddress;Database=myDataBase;Trusted_Connection=True;");
            dao.Setup(d => d.GetConnection()).Returns(connectionMock.Object);

            // Act
            var results = dao.Object.LoadCustomers();

            // Assert
            Assert.Equal(3, results.Count);
        }



        [Fact]
        public void TestGetCustomersWithNullEmailAddressWorks()
        {
            // Arrange
            Mock<IDataReader> readerMock = new Mock<IDataReader>();
            readerMock.Setup(r => r.Read()).ReturnsInOrder(true, true, true, false);
            readerMock.Setup(r => r.GetInt32(0)).ReturnsInOrder(1, 2, 3);
            readerMock.Setup(r => r.GetString(1)).ReturnsInOrder("John", "Jane", "Bob");
            readerMock.Setup(r => r.GetString(2)).ReturnsInOrder("Smith", "Carter", "Adams");
            readerMock.Setup(r => r.IsDBNull(3)).ReturnsInOrder(false, false, true);
            readerMock.Setup(r => r.GetString(3)).ReturnsInOrder("jsmith@xyz.com", "jcarter@abc.com", DBNull.Value);
            Mock<IDbCommand> mockCommand = new Mock<IDbCommand>();
            mockCommand.Setup(c => c.ExecuteReader()).Returns(readerMock.Object);
            Mock<IDbConnection> connectionMock = new Mock<IDbConnection>();
            connectionMock.Setup(c => c.CreateCommand()).Returns(mockCommand.Object);

            Mock<CustomerDao> dao = new Mock<CustomerDao>("Server=myServerAddress;Database=myDataBase;Trusted_Connection=True;");
            dao.Setup(d => d.GetConnection()).Returns(connectionMock.Object);

            // Act
            var results = dao.Object.LoadCustomers();

            // Assert
            Assert.Equal(3, results.Count);
        }

    }
}
