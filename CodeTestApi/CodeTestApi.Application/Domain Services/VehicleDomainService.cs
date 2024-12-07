using CodeTestApi.Domain.Entities;
using CodeTestApi.Domain.Interfaces;
using System.Security.Claims;

namespace CodeTestApi.Application.Domain_Services
{
    public class VehicleRentalDomainService : IVehicleDomainService
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleRentalDomainService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<Vehicle> GetVehicleOrThrowAsync(string vehicleId)
        {
            var vehicle = await _vehicleRepository.GetVehicleByIdAsync(vehicleId);
            if (vehicle is null)
            {
                throw new KeyNotFoundException("Vehicle not found");
            }
            return vehicle;
        }

        public async Task<string> ValidateUserDoesNotHasRentedVehiclesAsync(ClaimsPrincipal user)
        {
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
            {
                throw new UnauthorizedAccessException("User not found");
            }

            var hasRentedVehicles = await _vehicleRepository.VerifyRentedVehiclesByUserAsync(userId);
            if (hasRentedVehicles)
            {
                throw new InvalidOperationException("User can't rent more than one vehicle at a time");
            }

            return userId;
        }

        public async Task ValidateVehicleAvailabilityAsync(string vehicleId, DateTime startDate, DateTime endDate)
        {
            var isAvailable = await _vehicleRepository.VerifyVehiculeAvailabilityAsync(vehicleId, startDate, endDate);
            if (!isAvailable)
            {
                throw new InvalidOperationException("Vehicle is not available for the selected period of time");
            }
        }
    }

}
