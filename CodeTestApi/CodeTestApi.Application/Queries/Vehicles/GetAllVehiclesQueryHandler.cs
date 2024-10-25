using MediatR;
using CodeTestApi.Domain.Entities;
using CodeTestApi.Domain.Interfaces;
using CodeTestApi.Application.Base_Handlers;

namespace CodeTestApi.Application.Queries.Vehicles
{
    public class GetAllQuery : IRequest<IEnumerable<Vehicle>>
    {

    }
    public class GetAllVehiclesQueryHandler : BaseVehicleHandler<GetAllQuery, IEnumerable<Vehicle>>
    {
        public GetAllVehiclesQueryHandler(IVehicleRepository vehicleRepository): base(vehicleRepository)
        {
        }

        public override async Task<IEnumerable<Vehicle>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            return await _vehicleRepository.GetAllVehiclesAsync();
        }
    }
}
