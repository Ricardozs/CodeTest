using CodeTestApi.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System.Text.Json;

namespace IntegrationTests.Host.Controllers;
public class VehicleControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public VehicleControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAvailableVehicles_Should_Return_ValidData()
    {
        var response = await _client.GetAsync("/api/vehicles/available");
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();
        var vehicles = JsonSerializer.Deserialize<List<Vehicle>>(responseBody, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        Assert.NotNull(vehicles);
        Assert.NotEmpty(vehicles);
        Assert.All(vehicles, vehicle => Assert.False(string.IsNullOrEmpty(vehicle.Brand)));
    }

}

