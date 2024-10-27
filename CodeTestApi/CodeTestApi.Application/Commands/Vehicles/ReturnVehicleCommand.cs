using CodeTestApi.Application.Base_Handlers;
using CodeTestApi.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CodeTestApi.Application.Commands.Vehicles
{
    public class ReturnVehicleCommand : IRequest<Unit>
    {
        public string VehicleId { get; set; }
        public string UserId { get; set; }
        public ReturnVehicleCommand(string vehicleId, string userId) 
        {
            VehicleId = vehicleId;
            UserId = userId;
        }
    }

    public class ReturnVehicleHandler : BaseVehicleHandler<ReturnVehicleCommand, Unit>
    {
        public ReturnVehicleHandler(IVehicleRepository vehicleRepository) : base(vehicleRepository)
        {
            
        }

        public override async Task<Unit> Handle(ReturnVehicleCommand request, CancellationToken cancellationToken)
        {

            var vehicle = await _vehicleRepository.GetVehicleByIdAsync(request.VehicleId);
            if (vehicle is null)
            {
                throw new KeyNotFoundException("Vehicle not found");
            }

            await _vehicleRepository.ReturnVehicleAsync(request.VehicleId, request.UserId);

            return Unit.Value;
        }
    }
}
