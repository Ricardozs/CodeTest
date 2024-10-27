﻿using CodeTestApi.Application.Base_Handlers;
using CodeTestApi.Domain.Interfaces;
using MediatR;

namespace CodeTestApi.Application.Commands.Vehicles
{
    public class UpdateVehicleCommand : IRequest<Unit>
    {
        public required string Id { get; set; }
        public required string Brand { get; set; }
        public required string Model { get; set; }
        public long PrincePerDay { get; set; }
        public DateTime ManufactureDate { get; set; }
    }

    public class UpdateVehicleHandler : BaseVehicleHandler<UpdateVehicleCommand, Unit>
    {
        public UpdateVehicleHandler(IVehicleRepository vehicleRepository) : base(vehicleRepository)
        {
        }

        public override async Task<Unit> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleRepository.GetVehicleByIdAsync(request.Id);
            if (vehicle is null)
            {
                throw new KeyNotFoundException("Vehicle not found");
            }
            vehicle.Brand = request.Brand;
            vehicle.Model = request.Model;
            vehicle.ManufactureDate = request.ManufactureDate;
            vehicle.PricePerDay = request.PrincePerDay;

            await _vehicleRepository.UpdateVehicleAsync(vehicle);

            return Unit.Value;
        }
    }
}
