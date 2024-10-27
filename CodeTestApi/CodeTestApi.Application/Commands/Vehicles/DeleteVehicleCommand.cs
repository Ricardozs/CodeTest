using CodeTestApi.Application.Base_Handlers;
using CodeTestApi.Domain.Interfaces;
using MediatR;

namespace CodeTestApi.Application.Commands.Vehicles
{
    public class DeleteVehicleCommand : IRequest<Unit>
    {
        public string Id { get; set; }
        public DeleteVehicleCommand(string id)
        {
            Id = id;
        }
    }

    public class DeleteVehicleHandler : BaseVehicleHandler<DeleteVehicleCommand, Unit>
    {
        public DeleteVehicleHandler(IVehicleRepository vehicleRepository): base(vehicleRepository)
        {
        }

        public override async Task<Unit> Handle(DeleteVehicleCommand request, CancellationToken token)
        {
            var vehicle = await ValidateVehicleExists(request.Id);

            await _vehicleRepository.DeleteVehicleAsync(request.Id);
            return Unit.Value;
        }
    }
}
