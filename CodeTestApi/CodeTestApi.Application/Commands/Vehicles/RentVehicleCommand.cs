using CodeTestApi.Application.Base_Handlers;
using CodeTestApi.Domain.Interfaces;
using MediatR;
using System.Security.Claims;

namespace CodeTestApi.Application.Commands.Vehicles
{
    public class RentVehicleCommand : IRequest<Unit>
    {
        public string Id { get; set; }
        public ClaimsPrincipal ClaimsPrincipal { get; set; }
        public RentVehicleCommand(string id, ClaimsPrincipal claimsPrincipal)
        {
            Id = id;
            ClaimsPrincipal = claimsPrincipal;
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
                throw new UnauthorizedAccessException("Couldn't find user id");
            }

            var hasRentedVehicles = await _vehicleRepository.VerifyRentedVehiclesByUserAsync(userId);

            if (hasRentedVehicles) 
            {
                throw new InvalidOperationException("User can't rent more than one vehicle at a time");
            }

            var vehicle = await _vehicleRepository.GetVehicleByIdAsync(request.Id);
            if (vehicle is null)
            {
                throw new KeyNotFoundException();
            }
            vehicle.IsAvailable = false;
            vehicle.RentedBy = userId;

            await _vehicleRepository.UpdateVehicleAsync(vehicle);
            return Unit.Value;
        }
    }
}
