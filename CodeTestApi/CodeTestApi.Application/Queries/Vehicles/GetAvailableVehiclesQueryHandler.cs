using MediatR;
using CodeTestApi.Domain.Interfaces;
using CodeTestApi.Domain.Entities;
using CodeTestApi.Application.Base_Handlers;

namespace CodeTestApi.Application.Queries.Vehicles;
public class GetAvailableVehiclesQuery : IRequest<IEnumerable<Vehicle>>
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

public class GetAvailableVehiclesQueryHandler : BaseVehicleHandler<GetAvailableVehiclesQuery, IEnumerable<Vehicle>>
{
    public GetAvailableVehiclesQueryHandler(IVehicleRepository vehicleRepository): base(vehicleRepository)
    {
    }

    public override async Task<IEnumerable<Vehicle>> Handle(GetAvailableVehiclesQuery request, CancellationToken cancellationToken)
    {
        return await _vehicleRepository.GetAllAvailableVehiclesAsync(request.StartDate, request.EndDate);
    }
}
