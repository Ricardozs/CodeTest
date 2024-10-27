using CodeTestApi.Application.Base_Handlers;
using CodeTestApi.Domain.Entities;
using CodeTestApi.Domain.Interfaces;
using MediatR;
using System.Security.Claims;

namespace CodeTestApi.Application.Commands.Vehicles
{
    /// <summary>
    /// Command to rent a vehicle for a specific period.
    /// </summary>
    public class RentVehicleCommand : IRequest<Unit>
    {
        /// <summary>
        /// The unique identifier of the vehicle to be rented.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The start date of the rental period.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// The end date of the rental period.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// The ClaimsPrincipal representing the current user renting the vehicle.
        /// </summary>
        public ClaimsPrincipal ClaimsPrincipal { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RentVehicleCommand"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the vehicle to be rented.</param>
        /// <param name="startDate">The start date of the rental period.</param>
        /// <param name="endDate">The end date of the rental period.</param>
        /// <param name="claimsPrincipal">The ClaimsPrincipal representing the user renting the vehicle.</param>
        public RentVehicleCommand(string id, DateTime startDate, DateTime endDate, ClaimsPrincipal claimsPrincipal)
        {
            Id = id;
            ClaimsPrincipal = claimsPrincipal;
            StartDate = startDate;
            EndDate = endDate;
        }
    }

    /// <summary>
    /// Handler for processing the vehicle rental.
    /// </summary>
    public class RentVehicleHandler : BaseVehicleHandler<RentVehicleCommand, Unit>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RentVehicleHandler"/> class.
        /// </summary>
        /// <param name="vehicleRepository">The vehicle repository for handling data operations.</param>
        public RentVehicleHandler(IVehicleRepository vehicleRepository) : base(vehicleRepository)
        {
        }

        /// <summary>
        /// Handles the execution of the RentVehicleCommand by verifying vehicle availability and adding a rental period.
        /// </summary>
        /// <param name="request">The RentVehicleCommand containing the vehicle and rental period details.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>An empty Unit value upon successful rental.</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown when the user is not found.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the user has already rented a vehicle or the vehicle is unavailable.</exception>
        public override async Task<Unit> Handle(RentVehicleCommand request, CancellationToken cancellationToken)
        {
            var userId = request.ClaimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
            {
                throw new UnauthorizedAccessException("User not found");
            }

            var hasRentedVehicles = await _vehicleRepository.VerifyRentedVehiclesByUserAsync(userId);

            if (hasRentedVehicles)
            {
                throw new InvalidOperationException("User can't rent more than one vehicle at a time");
            }

            await ValidateVehicleExists(request.Id);

            var isVehiculeAvailable = await _vehicleRepository.VerifyVehiculeAvailabilityAsync(request.Id, request.StartDate, request.EndDate);
            if (!isVehiculeAvailable)
            {
                throw new InvalidOperationException("Vehicle is not available for the selected period of time");
            }

            var rentalPeriod = new RentalPeriod
            {
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                RentedBy = userId
            };

            await _vehicleRepository.RentVehicleAsync(request.Id, rentalPeriod);
            return Unit.Value;
        }
    }
}
