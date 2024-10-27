using CodeTestApi.Application.Base_Handlers;
using CodeTestApi.Domain.Interfaces;
using MediatR;

namespace CodeTestApi.Application.Commands.Vehicles
{
    /// <summary>
    /// Command to delete a vehicle from the system by its unique identifier.
    /// </summary>
    public class DeleteVehicleCommand : IRequest<Unit>
    {
        /// <summary>
        /// The unique identifier of the vehicle to be deleted.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteVehicleCommand"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the vehicle to be deleted.</param>
        public DeleteVehicleCommand(string id)
        {
            Id = id;
        }
    }

    /// <summary>
    /// Handler for deleting a vehicle from the system.
    /// </summary>
    public class DeleteVehicleHandler : BaseVehicleHandler<DeleteVehicleCommand, Unit>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteVehicleHandler"/> class.
        /// </summary>
        /// <param name="vehicleRepository">The vehicle repository for handling data operations.</param>
        public DeleteVehicleHandler(IVehicleRepository vehicleRepository) : base(vehicleRepository)
        {
        }

        /// <summary>
        /// Handles the execution of the DeleteVehicleCommand by removing the vehicle from the repository.
        /// </summary>
        /// <param name="request">The DeleteVehicleCommand containing the vehicle's unique identifier.</param>
        /// <param name="token">Cancellation token for the async operation.</param>
        /// <returns>An empty Unit value upon successful deletion.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the vehicle is not found.</exception>
        public override async Task<Unit> Handle(DeleteVehicleCommand request, CancellationToken token)
        {
            var vehicle = await ValidateVehicleExists(request.Id);

            await _vehicleRepository.DeleteVehicleAsync(request.Id);
            return Unit.Value;
        }
    }
}
