using CodeTestApi.Application.Base_Handlers;
using CodeTestApi.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTestApi.Application.Commands.Vehicles
{
    public class CancelRentCommand : IRequest<Unit>
    {
        public string VehicleId { get; set; }
        public string UserId { get; set; }
        public CancelRentCommand(string vehicleId, string userId) 
        {
            VehicleId = vehicleId;
            UserId = userId;
        }
    }

    public class CancelRentHandler : BaseVehicleHandler<CancelRentCommand, Unit>
    {
        public CancelRentHandler(IVehicleRepository vehicleRepository) : base(vehicleRepository)
        {
            
        }

        public override async Task<Unit> Handle(CancelRentCommand request, CancellationToken cancellationToken)
        {
            await ValidateVehicleExists(request.VehicleId);

            await _vehicleRepository.CancelRentAsync(request.VehicleId, request.UserId);

            return Unit.Value;
        }
    }
}
