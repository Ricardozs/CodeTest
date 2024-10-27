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
    public VehiclesController(IMediator mediator) : base(mediator)
    {
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("vehicle")]
    public async Task<IActionResult> CreateVehicle(CreateVehicleCommand command)
    {
        var vehicleId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetVehicle), new { id = vehicleId }, vehicleId);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAllVehicles()
    {
        var vehicles = await _mediator.Send(new GetAllQuery());
        return Ok(vehicles);
    }

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

    [AllowAnonymous]
    [HttpGet("available")]
    public async Task<IActionResult> GetAvailableVehicles([FromQuery] GetAvailableVehiclesQuery query)
    {
        var vehicles = await _mediator.Send(query);
        return Ok(vehicles);
    }

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
