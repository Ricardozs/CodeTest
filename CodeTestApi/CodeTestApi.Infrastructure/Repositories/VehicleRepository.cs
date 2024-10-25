using CodeTestApi.Domain.Entities;
using CodeTestApi.Domain.Interfaces;
using MongoDB.Driver;

namespace CodeTestApi.Infrastructure.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly IMongoCollection<Vehicle> _vehicles;

        public VehicleRepository(IMongoDatabase database)
        {
            _vehicles = database.GetCollection<Vehicle>("Vehicles");
        }

        public async Task<Vehicle> GetVehicleByIdAsync(string id)
        {
            return await _vehicles.Find(v => v.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Vehicle>> GetAllVehiclesAsync()
        {
            return await _vehicles.Find(v => true).ToListAsync();
        }

        public async Task<IEnumerable<Vehicle>> GetAllAvailableVehiclesAsync()
        {
            return await _vehicles.Find(v => v.IsAvailable).ToListAsync();
        }

        public async Task AddVehicleAsync(Vehicle vehicle)
        {
            await _vehicles.InsertOneAsync(vehicle);
        }

        public async Task UpdateVehicleAsync(Vehicle vehicle)
        {
            await _vehicles.ReplaceOneAsync(v => v.Id == vehicle.Id, vehicle);
        }

        public async Task DeleteVehicleAsync(string id)
        {
            await _vehicles.DeleteOneAsync(v => v.Id == id);
        }

        public async Task<bool> VerifyRentedVehiclesByUserAsync(string renterId)
        {
            return await _vehicles.Find(v => v.RentedBy == renterId).AnyAsync();
        }
    }
}
