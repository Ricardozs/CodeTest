using CodeTestApi.Domain.Entities;
using CodeTestApi.Infrastructure.Repositories;
using Mongo2Go;
using MongoDB.Driver;

namespace IntegrationTests.Infrastructure.Repositories;
public class VehicleRepositoryTests : IDisposable
{
    private readonly MongoDbRunner _runner;
    private readonly IMongoDatabase _database;
    private readonly VehicleRepository _repository;

    public VehicleRepositoryTests()
    {
        _runner = MongoDbRunner.Start();
        var client = new MongoClient(_runner.ConnectionString);
        _database = client.GetDatabase("IntegrationTestDb");
        _repository = new VehicleRepository(_database);
    }

    [Fact]
    public async Task AddVehicle_Should_Insert_And_Retrieve_Vehicle()
    {
        // Arrange
        var vehicle = new Vehicle
        {
            Id = Guid.NewGuid().ToString(),
            Brand = "Toyota",
            Model = "Corolla",
            ManufactureDate = DateTime.Now,
            IsAvailable = true
        };

        // Act
        await _repository.AddVehicleAsync(vehicle);
        var vehicles = await _repository.GetAllVehiclesAsync();

        // Assert
        Assert.Contains(vehicles, v => v.Brand == "Toyota");
    }

    public void Dispose()
    {
        _runner.Dispose();  // Limpia el servidor de prueba
    }
}
