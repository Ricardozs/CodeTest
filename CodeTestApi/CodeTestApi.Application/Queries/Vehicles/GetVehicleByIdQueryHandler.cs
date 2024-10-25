using CodeTestApi.Application.Base_Handlers;
using CodeTestApi.Domain.Entities;
using CodeTestApi.Domain.Interfaces;
using MediatR;

namespace CodeTestApi.Application.Queries.Vehicles
{
    public class GetVehicleByIdQuery : IRequest<Vehicle>
    {
        public string Id { get; set; }
        public GetVehicleByIdQuery(string id)
        {
            Id = id;
        }
    }
    public class GetVehicleByIdQueryHandler : BaseVehicleHandler<GetVehicleByIdQuery, Vehicle>
    {
        public GetVehicleByIdQueryHandler(IVehicleRepository vehicleRepository): base(vehicleRepository)
        {
        }

        public override async Task<Vehicle> Handle(GetVehicleByIdQuery request, CancellationToken token)
        {
            var vehicle = await _vehicleRepository.GetVehicleByIdAsync(request.Id);
            if (vehicle is null)
            {
                throw new KeyNotFoundException();
            }
            return vehicle;
        }
    }
}
