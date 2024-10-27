using MediatR;
using Microsoft.AspNetCore.Mvc;
using CodeTestApi.Application.Commands.Vehicles;
using CodeTestApi.Application.Queries.Vehicles;
using CodeTestApi.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using CodeTestApi.Host.DTO;
using CodeTestApi.Host.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class VehiclesController : RentalApiControllerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="VehiclesController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance for handling requests.</param>
    public VehiclesController(IMediator mediator) : base(mediator)
    {
    }

    /// <summary>
    /// Creates a new vehicle.
    /// </summary>
    /// <param name="command">The CreateVehicleCommand containing vehicle details.</param>
    /// <returns>A CreatedAtAction result with the ID of the newly created vehicle.</returns>
    [Authorize(Roles = "Admin")]
    [HttpPost("vehicle")]
    public async Task<IActionResult> CreateVehicle(CreateVehicleCommand command)
    {
        var vehicleId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetVehicle), new { id = vehicleId }, vehicleId);
    }

    /// <summary>
    /// Retrieves all vehicles.
    /// </summary>
    /// <returns>An Ok result containing all vehicles.</returns>
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAllVehicles()
    {
        var vehicles = await _mediator.Send(new GetAllQuery());
        return Ok(vehicles);
    }

    /// <summary>
    /// Retrieves a vehicle by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle.</param>
    /// <returns>An Ok result with the vehicle details or NotFound if the vehicle is not found.</returns>
    [AllowAnonymous]
    [HttpGet("vehicle/{id}")]
    public async Task<IActionResult> GetVehicle(string id)
    {
        try
        {
            var vehicle = await _mediator.Send(new GetVehicleByIdQuery(id));
            return Ok(vehicle);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Retrieves available vehicles for a specified date range.
    /// </summary>
    /// <param name="query">The query containing the start and end dates for availability.</param>
    /// <returns>An Ok result containing available vehicles.</returns>
    [AllowAnonymous]
    [HttpGet("available")]
    public async Task<IActionResult> GetAvailableVehicles([FromQuery] GetAvailableVehiclesQuery query)
    {
        var vehicles = await _mediator.Send(query);
        return Ok(vehicles);
    }

    /// <summary>
    /// Updates an existing vehicle's details.
    /// </summary>
    /// <param name="command">The UpdateVehicleCommand containing updated vehicle details.</param>
    /// <returns>A NoContent result if the update is successful or NotFound if the vehicle is not found.</returns>
    [Authorize(Roles = "Admin")]
    [HttpPut("vehicle")]
    public async Task<IActionResult> UpdateVehicle(UpdateVehicleCommand command)
    {
        try
        {
            await _mediator.Send(command);
            return NoContent();
        }
        catch (Exception)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Rents a vehicle for a specified period.
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle to rent.</param>
    /// <param name="request">The request containing the rental start and end dates.</param>
    /// <returns>A NoContent result if the rental is successful or NotFound if the vehicle is not found.</returns>
    [HttpPatch("vehicle/rent/{id}")]
    public async Task<IActionResult> RentVehicle(string id, [FromBody] RentVehicleRequest request)
    {
        try
        {
            await _mediator.Send(new RentVehicleCommand(id, request.StartDate, request.EndDate, User));
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Returns a rented vehicle.
    /// </summary>
    /// <param name="request">The request containing the vehicle and user details.</param>
    /// <returns>A NoContent result if the return is successful or NotFound if the vehicle is not found.</returns>
    [Authorize(Roles = "Admin")]
    [HttpPatch("vehicle/rent/return")]
    public async Task<IActionResult> ReturnVehicle([FromBody] RentedVehicle request)
    {
        try
        {
            await _mediator.Send(new ReturnVehicleCommand(request.VehicleId, request.UserId));
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Cancels an active vehicle rental.
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle whose rental is being canceled.</param>
    /// <returns>A NoContent result if the rental is successfully canceled or NotFound if the vehicle is not found.</returns>
    [Authorize(Roles = "Admin")]
    [HttpDelete("vehicle/rent/{id}")]
    public async Task<IActionResult> CancelRent(string id)
    {
        try
        {
            await _mediator.Send(new CancelRentCommand(id, User));
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Deletes a vehicle by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle to delete.</param>
    /// <returns>A NoContent result if the vehicle is successfully deleted or NotFound if the vehicle is not found.</returns>
    [Authorize(Roles = "Admin")]
    [HttpDelete("vehicle/{id}")]
    public async Task<IActionResult> DeleteVehicle(string id)
    {
        try
        {
            await _mediator.Send(new DeleteVehicleCommand(id));
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
