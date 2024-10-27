using CodeTestApi.Application.Base_Handlers;
using CodeTestApi.Domain.Interfaces;
using MediatR;

namespace CodeTestApi.Application.Commands.Vehicles
{
    /// <summary>
    /// Command to return a rented vehicle.
    /// </summary>
    public class ReturnVehicleCommand : IRequest<Unit>
    {
        /// <summary>
        /// The unique identifier of the vehicle being returned.
        /// </summary>
        public string VehicleId { get; set; }

        /// <summary>
        /// The unique identifier of the user returning the vehicle.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReturnVehicleCommand"/> class.
        /// </summary>
        /// <param name="vehicleId">The unique identifier of the vehicle being returned.</param>
        /// <param name="userId">The unique identifier of the user returning the vehicle.</param>
        public ReturnVehicleCommand(string vehicleId, string userId)
        {
            VehicleId = vehicleId;
            UserId = userId;
        }
    }

    /// <summary>
    /// Handler for processing the return of a rented vehicle.
    /// </summary>
    public class ReturnVehicleHandler : BaseVehicleHandler<ReturnVehicleCommand, Unit>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReturnVehicleHandler"/> class.
        /// </summary>
        /// <param name="vehicleRepository">The vehicle repository for handling data operations.</param>
        public ReturnVehicleHandler(IVehicleRepository vehicleRepository) : base(vehicleRepository)
        {
        }

        /// <summary>
        /// Handles the return of a rented vehicle by marking the rental period as inactive.
        /// </summary>
        /// <param name="request">The ReturnVehicleCommand containing the vehicle and user details.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>An empty Unit value upon successful return.</returns>
        public override async Task<Unit> Handle(ReturnVehicleCommand request, CancellationToken cancellationToken)
        {
            await ValidateVehicleExists(request.VehicleId);

            await _vehicleRepository.ReturnVehicleAsync(request.VehicleId, request.UserId);

            return Unit.Value;
        }
    }
}
