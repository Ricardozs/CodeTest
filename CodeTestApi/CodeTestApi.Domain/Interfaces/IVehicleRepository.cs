using CodeTestApi.Domain.Entities;

namespace CodeTestApi.Domain.Interfaces
{
    /// <summary>
    /// Repository interface for managing vehicle-related data operations.
    /// Provides methods to retrieve, add, update, and delete vehicles, as well as to handle vehicle rentals.
    /// </summary>
    public interface IVehicleRepository
    {
        /// <summary>
        /// Retrieves a vehicle by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the vehicle.</param>
        /// <returns>The vehicle with the specified id, or null if no vehicle is found.</returns>
        Task<Vehicle> GetVehicleByIdAsync(string id);

        /// <summary>
        /// Retrieves all vehicles in the system.
        /// </summary>
        /// <returns>An IEnumerable of all vehicles.</returns>
        Task<IEnumerable<Vehicle>> GetAllVehiclesAsync();

        /// <summary>
        /// Retrieves all available vehicles for a given date range.
        /// </summary>
        /// <param name="startDate">The start date of the rental period.</param>
        /// <param name="endDate">The end date of the rental period.</param>
        /// <returns>An IEnumerable of available vehicles within the specified date range.</returns>
        Task<IEnumerable<Vehicle>> GetAllAvailableVehiclesAsync(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Adds a new vehicle to the system.
        /// </summary>
        /// <param name="vehicle">The vehicle entity to be added.</param>
        Task AddVehicleAsync(Vehicle vehicle);

        /// <summary>
        /// Updates an existing vehicle in the system.
        /// </summary>
        /// <param name="vehicle">The vehicle entity with updated information.</param>
        Task UpdateVehicleAsync(Vehicle vehicle);

        /// <summary>
        /// Adds a rental period to a vehicle, marking it as rented.
        /// </summary>
        /// <param name="id">The unique identifier of the vehicle to rent.</param>
        /// <param name="newRentalPeriod">The rental period details.</param>
        Task RentVehicleAsync(string id, RentalPeriod newRentalPeriod);

        /// <summary>
        /// Marks a vehicle's rental period as returned by a specific user.
        /// </summary>
        /// <param name="vehicleId">The unique identifier of the vehicle.</param>
        /// <param name="userId">The unique identifier of the user who rented the vehicle.</param>
        Task ReturnVehicleAsync(string vehicleId, string userId);

        /// <summary>
        /// Cancels an active rental period for a specific vehicle and user.
        /// </summary>
        /// <param name="vehicleId">The unique identifier of the vehicle.</param>
        /// <param name="userId">The unique identifier of the user who rented the vehicle.</param>
        Task CancelRentAsync(string vehicleId, string userId);

        /// <summary>
        /// Deletes a vehicle from the system by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the vehicle to delete.</param>
        Task DeleteVehicleAsync(string id);

        /// <summary>
        /// Verifies if a user has any active vehicle rentals.
        /// </summary>
        /// <param name="renterId">The unique identifier of the user.</param>
        /// <returns>True if the user has an active rental, otherwise false.</returns>
        Task<bool> VerifyRentedVehiclesByUserAsync(string renterId);

        /// <summary>
        /// Verifies if a vehicle is available for rent within a specified date range.
        /// </summary>
        /// <param name="vehicleId">The unique identifier of the vehicle.</param>
        /// <param name="startDate">The start date of the rental period.</param>
        /// <param name="endDate">The end date of the rental period.</param>
        /// <returns>True if the vehicle is available, otherwise false.</returns>
        Task<bool> VerifyVehiculeAvailabilityAsync(string vehicleId, DateTime startDate, DateTime endDate);
    }
}
