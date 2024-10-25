using CodeTestApi.Domain.Interfaces;
using CodeTestApi.Domain.Entities;
using NSubstitute;
using CodeTestApi.Application.Commands.Vehicles;

namespace UnitTests.Application.Commands
{
    public class DeleteVehicleHandlerTests
    {
        private readonly IVehicleRepository _mockRepository = Substitute.For<IVehicleRepository>();
        private readonly DeleteVehicleHandler _handler;

        #region Test Data
        private readonly Vehicle _mockVehicle = new Vehicle
        {
            Id = "someId123",
            Brand = "someBrand",
            Model = "someModel",
            ManufactureDate = DateTime.Now,
            IsAvailable = true
        };
        #endregion

        public DeleteVehicleHandlerTests()
        {
            _mockRepository.GetVehicleByIdAsync("someId123").Returns(_mockVehicle);
            _handler = new DeleteVehicleHandler(_mockRepository);
        }

        [Fact]
        public async Task DeleteVehicle_ShoulCall_DeleteVehicleAsync()
        {
            var command = new DeleteVehicleCommand("someId123");

            await _handler.Handle(command, CancellationToken.None);

            await _mockRepository.Received(1).DeleteVehicleAsync(Arg.Any<string>());
        }

        [Fact]
        public async Task DeleteVehicle_ShoulThrowException_KeyNotFoundException()
        {
            var command = new DeleteVehicleCommand("notExistingId123");

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
