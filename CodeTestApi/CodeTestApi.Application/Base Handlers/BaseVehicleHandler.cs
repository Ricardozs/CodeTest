﻿using CodeTestApi.Domain.Entities;
using CodeTestApi.Domain.Interfaces;
using MediatR;

namespace CodeTestApi.Application.Base_Handlers
{
    public abstract class BaseVehicleHandler<T, Y> : IRequestHandler<T, Y> where T : IRequest<Y>
    {
        protected readonly IVehicleRepository _vehicleRepository;
        protected BaseVehicleHandler(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }
        public virtual Task<Y> Handle(T request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<Vehicle> ValidateVehicleExists(string vehicleId) 
        {
            var vehicle = await _vehicleRepository.GetVehicleByIdAsync(vehicleId);
            if (vehicle is null)
            {
                throw new KeyNotFoundException("Vehicle not found");
            }

            return vehicle;
        }
    }
}
