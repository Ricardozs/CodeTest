using CodeTestApi.Application.Base_Handlers;
using CodeTestApi.Domain.Entities;
using CodeTestApi.Domain.Interfaces;
using MediatR;
using System.Security.Claims;

namespace CodeTestApi.Application.Commands.Vehicles
{
    public class RentVehicleCommand : IRequest<Unit>
    {
        public string Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ClaimsPrincipal ClaimsPrincipal { get; set; }
        public RentVehicleCommand(string id, DateTime startDate, DateTime endDate, ClaimsPrincipal claimsPrincipal)
        {
            Id = id;
            ClaimsPrincipal = claimsPrincipal;
            StartDate = startDate;
            EndDate = endDate;
        }
    }

    public class RentVehicleHandler : BaseVehicleHandler<RentVehicleCommand, Unit>
    {
        public RentVehicleHandler(IVehicleRepository vehicleRepository):  base(vehicleRepository)
        {
        }

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
