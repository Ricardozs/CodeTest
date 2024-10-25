using CodeTestApi.Domain.Entities;
using CodeTestApi.Domain.Enums;
using CodeTestApi.Domain.Interfaces;

namespace CodeTestApi.Host
{
    public class DatabaseSeeder
    {
        public static async Task SeedVehiclesAsync(IVehicleRepository vehicleRepository)
        {
            var existingVehicles = await vehicleRepository.GetAllVehiclesAsync();

            // Si no hay vehículos, hacemos el seeding
            if (!existingVehicles.Any())
            {
                var seedVehicles = new List<Vehicle>
                {
                    new Vehicle
                    { 
                        Id = Guid.NewGuid().ToString(), 
                        Brand = "Toyota", 
                        Model = "Corolla", 
                        ManufactureDate = new DateTime(2020, 6, 12), 
                        IsAvailable = true 
                    },
                    new Vehicle 
                    { 
                        Id = Guid.NewGuid().ToString(), 
                        Brand = "Honda", 
                        Model = "Civic", 
                        ManufactureDate = new DateTime(2021, 5, 15), 
                        IsAvailable = true 
                    },
                    new Vehicle
                    {
                        Id = Guid.NewGuid().ToString(), 
                        Brand = "Ford", 
                        Model = "Figo", 
                        ManufactureDate = new DateTime(2019, 8, 20), 
                        IsAvailable = true 
                    }
                };

                foreach (var vehicle in seedVehicles)
                {
                    await vehicleRepository.AddVehicleAsync(vehicle);
                }
            }
        }

        public static async Task SeedUsersAsync(IUserRepository userRepository)
        {
            var existingUsers = await userRepository.GetAllUsersAsync();

            if (!existingUsers.Any())
            {
                var adminPassword = BCrypt.Net.BCrypt.HashPassword("AdminPassword");
                var userPassword = BCrypt.Net.BCrypt.HashPassword("UserPassword");
                var adminUser = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = "Admin",
                    LastName = "User",
                    Email = "admin@example.com",
                    Phone = "555-1234",
                    HashPassword = adminPassword,
                    UserType = UserType.Admin
                };

                var standardUser = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = "Standard",
                    LastName = "User",
                    Email = "user@example.com",
                    Phone = "555-5678",
                    HashPassword = userPassword,
                    UserType = UserType.User
                };
                await userRepository.AddUserAsync(adminUser);
                await userRepository.AddUserAsync(standardUser);
            }
        }
    }
}
