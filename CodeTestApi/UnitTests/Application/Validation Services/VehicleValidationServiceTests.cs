using CodeTestApi.Application.Helpers;
using System;
using Xunit;

namespace CodeTestApi.Tests.Validation_Services
{
    public class VehicleValidationServiceTests
    {
        public static TheoryData<DateTime, bool> ManufactureDateTestData => new TheoryData<DateTime, bool>
        {
            { DateTime.Now.AddYears(-3), true },
            { DateTime.Now.AddYears(-5).AddDays(1), true },
            { DateTime.Now.AddYears(-6), false }
        };

        [Theory]
        [MemberData(nameof(ManufactureDateTestData))]
        public void IsVehicleManufactureDateValid_ShouldReturnExpectedResult(DateTime manufactureDate, bool expected)
        {
            // Act
            var result = VehicleValidationService.IsVehicleManufactureDateValid(manufactureDate);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
