using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;

namespace IntegrationTests.Host.Controllers;
public class VehicleControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public VehicleControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAvailableVehicles_Should_Return_Ok()
    {
        // Act
        var response = await _client.GetAsync("/api/vehicles/available");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}

