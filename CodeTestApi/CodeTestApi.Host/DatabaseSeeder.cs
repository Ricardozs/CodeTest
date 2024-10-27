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
                        RentalPeriods = [],
                        PricePerDay = 20
                    },
                    new Vehicle
                    {
                        Id = Guid.NewGuid().ToString(),
                        Brand = "Honda",
                        Model = "Civic",
                        ManufactureDate = new DateTime(2021, 5, 15),
                        RentalPeriods = [],
                        PricePerDay = 30
                    },
                    new Vehicle
                    {
                        Id = Guid.NewGuid().ToString(),
                        Brand = "Ford",
                        Model = "Figo",
                        ManufactureDate = new DateTime(2019, 8, 20),
                        RentalPeriods = [],
                        PricePerDay = 40
                    }
                };

                foreach (var vehicle in seedVehicles)
                {
                    await vehicleRepository.AddVehicleAsync(vehicle);
                }
                Console.WriteLine("Vehicle Data Seeding Succesful");
            }
        }

        public static async Task SeedUsersAsync(IUserRepository userRepository)
        {
            var existingUsers = await userRepository.GetAllUsersAsync();

            if (!existingUsers.Any())
            {
                var adminPassword = BCrypt.Net.BCrypt.HashPassword("AdminPassword");
                var userPassword = BCrypt.Net.BCrypt.HashPassword("UserPassword");
                var seedUsers = new List<User>
                {
                    new User
                    {
                        Id = Guid.NewGuid().ToString(),
                        FirstName = "Admin",
                        LastName = "User",
                        Email = "admin@example.com",
                        Phone = "674-124567",
                        HashPassword = adminPassword,
                        UserType = UserType.Admin
                    },
                    new User
                    {
                        Id = Guid.NewGuid().ToString(),
                        FirstName = "Ricardo",
                        LastName = "Sanchez",
                        Email = "ric@gmail.com",
                        Phone = "674-567812",
                        HashPassword = userPassword,
                        UserType = UserType.User
                    },
                    new User
                    {
                        Id = Guid.NewGuid().ToString(),
                        FirstName = "Juan",
                        LastName = "Perez",
                        Email = "juan@gmail.com",
                        Phone = "674-567812",
                       HashPassword = userPassword,
                        UserType = UserType.User
                    },
                    new User
                    {
                        Id = Guid.NewGuid().ToString(),
                        FirstName = "Roberto",
                        LastName = "Gonzalez",
                        Email = "rob@gmail.com",
                        Phone = "674-567812",
                        HashPassword = userPassword,
                        UserType = UserType.User
                    }
                };
                foreach (var user in seedUsers)
                {
                    await userRepository.AddUserAsync(user);
                }
                Console.WriteLine("User Data Seeding Succesful");
            }
        }
    }
}
