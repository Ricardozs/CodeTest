using CodeTestApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CodeTestApi.Domain.Interfaces
{
    public interface IVehicleDomainService
    {
        Task<Vehicle> GetVehicleOrThrowAsync(string vehicleId);
        Task<string> ValidateUserDoesNotHasRentedVehiclesAsync(ClaimsPrincipal user);
        Task ValidateVehicleAvailabilityAsync(string vehicleId, DateTime startDate, DateTime endDate);
    }
}
