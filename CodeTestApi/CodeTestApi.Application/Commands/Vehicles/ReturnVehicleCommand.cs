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
    public class ReturnVehicleCommand : IRequest<Unit>
    {
        public string Id { get; set; }
        public ReturnVehicleCommand(string id) 
        {
            Id = id;
        }
    }

    public class ReturnVehicleHandler : BaseVehicleHandler<ReturnVehicleCommand, Unit>
    {
        public ReturnVehicleHandler(IVehicleRepository vehicleRepository) : base(vehicleRepository)
        {
            
        }

        public override async Task<Unit> Handle(ReturnVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleRepository.GetVehicleByIdAsync(request.Id);
            if (vehicle is null)
            {
                throw new KeyNotFoundException();
            }

            vehicle.IsAvailable = true;
            vehicle.RentedBy = null;

            await _vehicleRepository.UpdateVehicleAsync(vehicle);

            return Unit.Value;
        }
    }
}
