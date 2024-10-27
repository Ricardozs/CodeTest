using CodeTestApi.Application.Base_Handlers;
using CodeTestApi.Domain.Interfaces;
using MediatR;
using System.Security.Claims;

namespace CodeTestApi.Application.Commands.Vehicles
{
    public class CancelRentCommand : IRequest<Unit>
    {
        public string VehicleId { get; set; }
        public ClaimsPrincipal ClaimsPrincipal { get; set; }
        public CancelRentCommand(string vehicleId, ClaimsPrincipal claimsPrincipal) 
        {
            VehicleId = vehicleId;
            ClaimsPrincipal = claimsPrincipal;
        }
    }

    public class CancelRentHandler : BaseVehicleHandler<CancelRentCommand, Unit>
    {
        public CancelRentHandler(IVehicleRepository vehicleRepository) : base(vehicleRepository)
        {
            
        }

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
