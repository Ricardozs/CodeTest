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
            ConfigureIndexes();
        }

        private void ConfigureIndexes()
        {
            var indexKeys = Builders<Vehicle>.IndexKeys.Combine(
                Builders<Vehicle>.IndexKeys.Ascending(v => v.RentalPeriods[0].StartDate),
                Builders<Vehicle>.IndexKeys.Ascending(v => v.RentalPeriods[0].EndDate),
                Builders<Vehicle>.IndexKeys.Ascending(v => v.RentalPeriods[0].RentedBy),
                Builders<Vehicle>.IndexKeys.Ascending(v => v.RentalPeriods[0].IsActive)
            );

            _vehicles.Indexes.CreateOne(new CreateIndexModel<Vehicle>(indexKeys));
        }

        public async Task<Vehicle> GetVehicleByIdAsync(string id)
        {
            return await _vehicles.Find(v => v.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Vehicle>> GetAllVehiclesAsync()
        {
            return await _vehicles.Find(v => true).ToListAsync();
        }

        public async Task<IEnumerable<Vehicle>> GetAllAvailableVehiclesAsync(DateTime startDate, DateTime endDate)
        {
            var filter = Builders<Vehicle>.Filter.And(
                Builders<Vehicle>.Filter.Not(
                    Builders<Vehicle>.Filter.ElemMatch(v => v.RentalPeriods,
                        Builders<RentalPeriod>.Filter.And(
                            Builders<RentalPeriod>.Filter.Eq(p => p.IsActive, true),
                            Builders<RentalPeriod>.Filter.Lt(p => p.StartDate, endDate.Date),
                            Builders<RentalPeriod>.Filter.Gt(p => p.EndDate, startDate.Date)
                        )
                    )
                )
            );
            return await _vehicles.Find(filter).ToListAsync();
        }

        public async Task AddVehicleAsync(Vehicle vehicle)
        {
            await _vehicles.InsertOneAsync(vehicle);
        }

        public async Task UpdateVehicleAsync(Vehicle vehicle)
        {
            await _vehicles.ReplaceOneAsync(v => v.Id == vehicle.Id, vehicle);
        }

        public async Task RentVehicleAsync(string id, RentalPeriod newRentalPeriod)
        {
            var update = Builders<Vehicle>.Update.Push(v => v.RentalPeriods, newRentalPeriod);

            await _vehicles.FindOneAndUpdateAsync(
                v => v.Id == id,
                update
            );
        }

        public async Task ReturnVehicleAsync(string vehicleId, string userId)
        {
            await _vehicles.FindOneAndUpdateAsync(
                v => v.Id == vehicleId && v.RentalPeriods.Any(p => p.RentedBy == userId && p.IsActive),
                Builders<Vehicle>.Update.Set("RentalPeriods.$.IsActive", false));
        }

        public async Task CancelRentAsync(string vehicleId, string userId)
        {
            var update = Builders<Vehicle>.Update.PullFilter(v => v.RentalPeriods, p => p.IsActive);

            await _vehicles.FindOneAndUpdateAsync(
                v => v.Id == vehicleId,
                update
            );
        }

        public async Task DeleteVehicleAsync(string id)
        {
            await _vehicles.DeleteOneAsync(v => v.Id == id);
        }

        public async Task<bool> VerifyRentedVehiclesByUserAsync(string renterId)
        {
            return await _vehicles.Find(v => v.RentalPeriods.Any(p => p.RentedBy == renterId && p.IsActive)).AnyAsync();
        }

        public async Task<bool> VerifyVehiculeAvailabilityAsync(string vehicleId, DateTime startDate, DateTime endDate)
        {
            var filter = Builders<Vehicle>.Filter.And(
                Builders<Vehicle>.Filter.Eq(v => v.Id, vehicleId),
                Builders<Vehicle>.Filter.Not(
                    Builders<Vehicle>.Filter.ElemMatch(v => v.RentalPeriods,
                        Builders<RentalPeriod>.Filter.And(
                            Builders<RentalPeriod>.Filter.Eq(p => p.IsActive, true),
                            Builders<RentalPeriod>.Filter.Lt(p => p.StartDate, endDate.Date),
                            Builders<RentalPeriod>.Filter.Gt(p => p.EndDate, startDate.Date)
                        )
                    )
                )
            );
            return await _vehicles.Find(filter).AnyAsync();
        }
    }
}
