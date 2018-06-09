using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ScheduleAppointment.Tests
{
    public class AppointmentServiceWrapperInterfaceTests
    {


        [Fact]
        public void WhenAppointmentDateOnMaxDate_AppointmentShouldBeCreatedForThatDate()
        {
            // Values
            DateTime now = new DateTime(2018, 5, 15, 11, 42, 32);
            DateTime today = now.Date;
            String maxAppointmentDays = "15";
            AppointmentRequest request = new AppointmentRequest()
            {
                AppointmentDate = new DateTime(2018, 5, 30),
                Hours = 4
            };

            // Arrange
            var dateTimeMock = new Mock<IDateTimeProvider>();
            dateTimeMock.Setup(m => m.Today).Returns(today);
            dateTimeMock.Setup(m => m.Now).Returns(now);
            var configMock = new Mock<IConfigProvider>();
            configMock.Setup(m => m.GetValueWithDefault(It.Is<String>(s => s == "MaxDays"), It.IsAny<String>()))
                .Returns(maxAppointmentDays);
            var confirmCodeMock = new Mock<IConfirmationCodeProvider>();
            confirmCodeMock.Setup(m => m.NewConfirmationCode()).Returns("ABCDEFG");
            var userMock = new Mock<IUserProvider>();
            userMock.Setup(m => m.CurrentUsername).Returns("SomeUser");

            // Act
            var apptService = new AppointmentServiceWithWrapperInterface(dateTimeMock.Object,
                configMock.Object, userMock.Object, confirmCodeMock.Object);
            var appt = apptService.CreateAppointment(request);

            // Assert
            Assert.NotNull(appt);
            Assert.Equal(new DateTime(2018, 5, 30), appt.AppointmentDate);
            Assert.Equal("ABCDEFG", appt.ConfirmationCode);
            Assert.Equal(now, appt.CreateTime);
        }

    }
}
