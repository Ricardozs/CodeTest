using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CodeTestApi.Application.Domain_Services;
using CodeTestApi.Domain.Entities;
using CodeTestApi.Domain.Interfaces;
using NSubstitute;
using Xunit;

namespace CodeTestApi.Tests.Domain_Services
{
    public class VehicleRentalDomainServiceTests
    {
        private readonly IVehicleRepository _mockRepository;
        private readonly VehicleDomainService _service;

        public VehicleRentalDomainServiceTests()
        {
            _mockRepository = Substitute.For<IVehicleRepository>();
            _service = new VehicleDomainService(_mockRepository);
        }

        [Fact]
        public async Task GetVehicleOrThrowAsync_ShouldReturnVehicle_WhenVehicleExists()
        {
            // Arrange
            var vehicleId = "vehicle123";
            var mockVehicle = new Vehicle 
            { 
                Id = vehicleId, 
                Brand = "someBrand", 
                Model = "someModel", 
                ManufactureDate = DateTime.Now 
            };
            _mockRepository.GetVehicleByIdAsync(vehicleId).Returns(mockVehicle);

            // Act
            var result = await _service.GetVehicleOrThrowAsync(vehicleId);

            // Assert
            Assert.Equal(vehicleId, result.Id);
        }

        [Fact]
        public async Task GetVehicleOrThrowAsync_ShouldThrowKeyNotFoundException_WhenVehicleDoesNotExist()
        {
            // Arrange
            var vehicleId = "nonexistent123";

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetVehicleOrThrowAsync(vehicleId));
        }

        [Fact]
        public async Task ValidateUserHasRentedVehiclesAsync_ShouldReturnUserId_WhenUserIsValid()
        {
            // Arrange
            var userId = "user123";
            var mockClaimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(
            [
                new Claim(ClaimTypes.NameIdentifier, userId)
            ]));
            _mockRepository.VerifyRentedVehiclesByUserAsync(userId).Returns(false);

            // Act
            var result = await _service.ValidateUserDoesNotHasRentedVehiclesAsync(mockClaimsPrincipal);

            // Assert
            Assert.Equal(userId, result);
        }

        [Fact]
        public async Task ValidateUserHasRentedVehiclesAsync_ShouldThrowUnauthorizedAccessException_WhenUserIsNotAuthenticated()
        {
            // Arrange
            var mockClaimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());

            // Act & Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _service.ValidateUserDoesNotHasRentedVehiclesAsync(mockClaimsPrincipal));
        }

        [Fact]
        public async Task ValidateUserHasRentedVehiclesAsync_ShouldThrowInvalidOperationException_WhenUserAlreadyHasRentedVehicles()
        {
            // Arrange
            var userId = "user123";
            var mockClaimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId)
            }));
            _mockRepository.VerifyRentedVehiclesByUserAsync(userId).Returns(true);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.ValidateUserDoesNotHasRentedVehiclesAsync(mockClaimsPrincipal));
        }

        [Fact]
        public async Task ValidateVehicleAvailabilityAsync_ShouldNotThrowException_WhenVehicleIsAvailable()
        {
            // Arrange
            var vehicleId = "vehicle123";
            var startDate = DateTime.Now;
            var endDate = DateTime.Now.AddDays(5);
            _mockRepository.VerifyVehiculeAvailabilityAsync(vehicleId, startDate, endDate).Returns(true);

            // Act
            await _service.ValidateVehicleAvailabilityAsync(vehicleId, startDate, endDate);

            // Assert
            // No exception should be thrown
        }

        [Fact]
        public async Task ValidateVehicleAvailabilityAsync_ShouldThrowInvalidOperationException_WhenVehicleIsNotAvailable()
        {
            // Arrange
            var vehicleId = "vehicle123";
            var startDate = DateTime.Now;
            var endDate = DateTime.Now.AddDays(5);
            _mockRepository.VerifyVehiculeAvailabilityAsync(vehicleId, startDate, endDate).Returns(false);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.ValidateVehicleAvailabilityAsync(vehicleId, startDate, endDate));
        }
    }
}
