using MediatR;
using Microsoft.AspNetCore.Mvc;
using CodeTestApi.Application.Commands.Vehicles;
using CodeTestApi.Application.Queries.Vehicles;
using CodeTestApi.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using CodeTestApi.Host.DTO;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class VehiclesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<VehiclesController> _logger;

    public VehiclesController(IMediator mediator, ILogger<VehiclesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
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
    [HttpPatch("vehicle/return/{vehicleId}")]
    public async Task<IActionResult> ReturnVehicle(string vehicleId, [FromBody] string userdId)
    {
        try
        {
            await _mediator.Send(new ReturnVehicleCommand(vehicleId, userdId));
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
