using System;
using Xunit;
using Moq;

namespace AddLocationExample.Tests
{
    public class LocationServiceTests
    {



        public readonly GeocodeResult FIELD_MUSEUM = new GeocodeResult()
        {
            StreetAddress = "1400 S Lakeshore Dr",
            City = "Chicago",
            State = "IL",
            ZipCode = "60605",
            FormattedAddress = "1400 S Lake Shore Dr, Chicago, IL 60605, USA"
        };

        [Fact]
        public void WhenAddressDoesNotExist_ItShouldBeAdded()
        {
            // Arrange
            var geocodeServiceMock = new Mock<IGeocodeService>();
            geocodeServiceMock.Setup(m => m.GeocodeAddress(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>()))
                .Returns(FIELD_MUSEUM);

            var locationRepositoryMock = new Mock<ILocationRepository>();
            locationRepositoryMock.Setup(m => m.ContainsAddress(It.IsAny<String>()))
                .Returns(false);

            LocationService service = new LocationService(geocodeServiceMock.Object, locationRepositoryMock.Object);

            // Act
            service.AddLocation("Field Museum", "1400 S Lakeshore", "Chicago", "IL");


            // Assert
            locationRepositoryMock.Verify(m => m.AddLocation(It.IsAny<Location>()), Times.Once);
        }


    }
}
