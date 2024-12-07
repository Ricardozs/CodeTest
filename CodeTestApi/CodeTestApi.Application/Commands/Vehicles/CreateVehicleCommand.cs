using CodeTestApi.Application.Base_Handlers;
using CodeTestApi.Application.Validation_Services;
using CodeTestApi.Domain.Entities;
using CodeTestApi.Domain.Interfaces;
using MediatR;

namespace CodeTestApi.Application.Commands.Vehicles;

/// <summary>
/// Command to create a new vehicle in the system.
/// </summary>
public class CreateVehicleCommand : IRequest<string>
{
    /// <summary>
    /// The brand of the vehicle.
    /// </summary>
    public required string Brand { get; set; }

    /// <summary>
    /// The model of the vehicle.
    /// </summary>
    public required string Model { get; set; }

    /// <summary>
    /// The manufacture date of the vehicle.
    /// </summary>
    public DateTime ManufactureDate { get; set; }

    /// <summary>
    /// The rental price per day for the vehicle.
    /// </summary>
    public long PricePerDay { get; set; }
}

/// <summary>
/// Handler for creating a new vehicle in the system.
/// </summary>
public class CreateVehicleHandler : BaseVehicleHandler<CreateVehicleCommand, string>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateVehicleHandler"/> class.
    /// </summary>
    /// <param name="vehicleRepository">The vehicle repository for handling data operations.</param>
    public CreateVehicleHandler(IVehicleRepository vehicleRepository, IVehicleDomainService vehicleDomainService) : base(vehicleRepository, vehicleDomainService)
    {
    }

    /// <summary>
    /// Handles the execution of the CreateVehicleCommand by adding a new vehicle to the repository.
    /// </summary>
    /// <param name="request">The CreateVehicleCommand containing the vehicle's details.</param>
    /// <param name="cancellationToken">Cancellation token for the async operation.</param>
    /// <returns>The unique identifier (ID) of the newly created vehicle.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the vehicle is more than 5 years old.</exception>
    public override async Task<string> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
    {
        var isVehicleManufactureDateValid = VehicleValidationService.IsVehicleManufactureDateValid(request.ManufactureDate);
        if (isVehicleManufactureDateValid)
        {
            throw new InvalidOperationException("Vehicle cannot be more than 5 years older");
        }

        var vehicle = new Vehicle
        {
            Id = Guid.NewGuid().ToString(),
            Brand = request.Brand,
            Model = request.Model,
            ManufactureDate = request.ManufactureDate,
            PricePerDay = request.PricePerDay,
            RentalPeriods = []
        };

        await _vehicleRepository.AddVehicleAsync(vehicle);

        return vehicle.Id;
    }
}
