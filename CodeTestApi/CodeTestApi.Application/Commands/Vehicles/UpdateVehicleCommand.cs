using CodeTestApi.Application.Base_Handlers;
using CodeTestApi.Domain.Interfaces;
using MediatR;

namespace CodeTestApi.Application.Commands.Vehicles
{
    /// <summary>
    /// Command to update an existing vehicle's details in the system.
    /// </summary>
    public class UpdateVehicleCommand : IRequest<Unit>
    {
        /// <summary>
        /// The unique identifier of the vehicle to be updated.
        /// </summary>
        public required string Id { get; set; }

        /// <summary>
        /// The updated brand of the vehicle.
        /// </summary>
        public required string Brand { get; set; }

        /// <summary>
        /// The updated model of the vehicle.
        /// </summary>
        public required string Model { get; set; }

        /// <summary>
        /// The updated price per day for renting the vehicle.
        /// </summary>
        public long PricePerDay { get; set; }

        /// <summary>
        /// The updated manufacture date of the vehicle.
        /// </summary>
        public DateTime ManufactureDate { get; set; }
    }

    /// <summary>
    /// Handler for updating an existing vehicle's details in the system.
    /// </summary>
    public class UpdateVehicleHandler : BaseVehicleHandler<UpdateVehicleCommand, Unit>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateVehicleHandler"/> class.
        /// </summary>
        /// <param name="vehicleRepository">The vehicle repository for handling data operations.</param>
        public UpdateVehicleHandler(IVehicleRepository vehicleRepository) : base(vehicleRepository)
        {
        }

        /// <summary>
        /// Handles the execution of the UpdateVehicleCommand by updating the vehicle's details in the repository.
        /// </summary>
        /// <param name="request">The UpdateVehicleCommand containing the updated vehicle details.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>An empty Unit value upon successful update.</returns>
        public override async Task<Unit> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await ValidateVehicleExists(request.Id);

            vehicle.Brand = request.Brand;
            vehicle.Model = request.Model;
            vehicle.ManufactureDate = request.ManufactureDate;
            vehicle.PricePerDay = request.PricePerDay;

            await _vehicleRepository.UpdateVehicleAsync(vehicle);

            return Unit.Value;
        }
    }
}
