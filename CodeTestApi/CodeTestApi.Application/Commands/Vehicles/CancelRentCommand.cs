using CodeTestApi.Application.Base_Handlers;
using CodeTestApi.Domain.Interfaces;
using MediatR;
using System.Security.Claims;

namespace CodeTestApi.Application.Commands.Vehicles
{
    /// <summary>
    /// Command to cancel an active vehicle rental.
    /// </summary>
    public class CancelRentCommand : IRequest<Unit>
    {
        /// <summary>
        /// The unique identifier of the vehicle whose rental is to be canceled.
        /// </summary>
        public string VehicleId { get; set; }

        /// <summary>
        /// The ClaimsPrincipal representing the current user.
        /// </summary>
        public ClaimsPrincipal ClaimsPrincipal { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CancelRentCommand"/> class.
        /// </summary>
        /// <param name="vehicleId">The unique identifier of the vehicle to cancel the rental for.</param>
        /// <param name="claimsPrincipal">The ClaimsPrincipal representing the current user.</param>
        public CancelRentCommand(string vehicleId, ClaimsPrincipal claimsPrincipal)
        {
            VehicleId = vehicleId;
            ClaimsPrincipal = claimsPrincipal;
        }
    }

    /// <summary>
    /// Handler for canceling an active vehicle rental.
    /// </summary>
    public class CancelRentHandler : BaseVehicleHandler<CancelRentCommand, Unit>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CancelRentHandler"/> class.
        /// </summary>
        /// <param name="vehicleRepository">The vehicle repository for handling data operations.</param>
        public CancelRentHandler(IVehicleRepository vehicleRepository) : base(vehicleRepository)
        {
        }

        /// <summary>
        /// Handles the cancellation of a vehicle rental by the current user.
        /// </summary>
        /// <param name="request">The CancelRentCommand containing the vehicle ID and user information.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>An empty Unit value upon successful cancellation.</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown when the current user is not found.</exception>
        public override async Task<Unit> Handle(CancelRentCommand request, CancellationToken cancellationToken)
        {
            var userId = request.ClaimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
            {
                throw new UnauthorizedAccessException("User not found");
            }

            await ValidateVehicleExists(request.VehicleId);

            await _vehicleRepository.CancelRentAsync(request.VehicleId, userId);

            return Unit.Value;
        }
    }
}
