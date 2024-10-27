using CodeTestApi.Application.Base_Handlers;
using CodeTestApi.Domain.Entities;
using CodeTestApi.Domain.Interfaces;
using MediatR;

namespace CodeTestApi.Application.Commands.Vehicles;
public class CreateVehicleCommand : IRequest<Vehicle>
{
    public required string Brand { get; set; }
    public required string Model { get; set; }
    public DateTime ManufactureDate { get; set; }
    public long PricePerDay { get; set; }
}

public class CreateVehicleHandler : BaseVehicleHandler<CreateVehicleCommand, Vehicle>
{
    public CreateVehicleHandler(IVehicleRepository vehicleRepository): base(vehicleRepository)
    {
    }

    public override async Task<Vehicle> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
    {
        var currentYear = DateTime.Now.Year;
        if (request.ManufactureDate.Year < currentYear - 5)
        {
            throw new InvalidOperationException("Vehicule cannot be more than 5 years older");
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

        return vehicle;
    }
}
