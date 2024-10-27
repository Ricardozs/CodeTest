using CodeTestApi.Domain.Entities;

namespace CodeTestApi.Domain.Interfaces
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetVehicleByIdAsync(string id);
        Task<IEnumerable<Vehicle>> GetAllVehiclesAsync();
        Task<IEnumerable<Vehicle>> GetAllAvailableVehiclesAsync(DateTime startDate, DateTime endDate);
        Task AddVehicleAsync(Vehicle vehicle);
        Task UpdateVehicleAsync(Vehicle vehicle);
        Task RentVehicleAsync(string id, RentalPeriod newRentalPeriod);
        Task ReturnVehicleAsync(string vehicleId, string userId);
        Task CancelRentAsync(string vehicleId, string userId);
        Task DeleteVehicleAsync(string id);
        Task<bool> VerifyRentedVehiclesByUserAsync(string renterId);
        Task<bool> VerifyVehiculeAvailabilityAsync(string vehicleId, DateTime startDate, DateTime endDate);
    }
}
