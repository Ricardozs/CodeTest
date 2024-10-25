using CodeTestApi.Domain.Entities;

namespace CodeTestApi.Domain.Interfaces
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetVehicleByIdAsync(string id);
        Task<IEnumerable<Vehicle>> GetAllVehiclesAsync();
        Task<IEnumerable<Vehicle>> GetAllAvailableVehiclesAsync();
        Task AddVehicleAsync(Vehicle vehicle);
        Task UpdateVehicleAsync(Vehicle vehicle);
        Task DeleteVehicleAsync(string id);
        Task<bool> VerifyRentedVehiclesByUserAsync(string renterId);
    }
}
