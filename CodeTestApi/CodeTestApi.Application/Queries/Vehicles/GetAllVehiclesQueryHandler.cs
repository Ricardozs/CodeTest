using MediatR;
using CodeTestApi.Domain.Entities;
using CodeTestApi.Domain.Interfaces;
using CodeTestApi.Application.Base_Handlers;

namespace CodeTestApi.Application.Queries.Vehicles
{
    /// <summary>
    /// Query to retrieve all vehicles in the system.
    /// </summary>
    public class GetAllQuery : IRequest<IEnumerable<Vehicle>>
    {
        // No parameters needed for retrieving all vehicles.
    }

    /// <summary>
    /// Handler for processing the retrieval of all vehicles.
    /// </summary>
    public class GetAllVehiclesQueryHandler : BaseVehicleHandler<GetAllQuery, IEnumerable<Vehicle>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllVehiclesQueryHandler"/> class.
        /// </summary>
        /// <param name="vehicleRepository">The vehicle repository for handling data operations.</param>
        public GetAllVehiclesQueryHandler(IVehicleRepository vehicleRepository, IVehicleDomainService vehicleDomainService) : base(vehicleRepository, vehicleDomainService)
        {
        }

        /// <summary>
        /// Handles the execution of the GetAllQuery by retrieving all vehicles from the repository.
        /// </summary>
        /// <param name="request">The GetAllQuery, which does not require any parameters.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>An IEnumerable of all vehicles in the system.</returns>
        public override async Task<IEnumerable<Vehicle>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            return await _vehicleRepository.GetAllVehiclesAsync();
        }
    }
}
