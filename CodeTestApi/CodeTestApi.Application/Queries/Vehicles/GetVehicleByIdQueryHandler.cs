using CodeTestApi.Application.Base_Handlers;
using CodeTestApi.Domain.Entities;
using CodeTestApi.Domain.Interfaces;
using MediatR;

namespace CodeTestApi.Application.Queries.Vehicles
{
    /// <summary>
    /// Query to retrieve a vehicle by its unique identifier.
    /// </summary>
    public class GetVehicleByIdQuery : IRequest<Vehicle>
    {
        /// <summary>
        /// The unique identifier of the vehicle to retrieve.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetVehicleByIdQuery"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the vehicle to retrieve.</param>
        public GetVehicleByIdQuery(string id)
        {
            Id = id;
        }
    }

    /// <summary>
    /// Handler for processing the retrieval of a vehicle by its unique identifier.
    /// </summary>
    public class GetVehicleByIdQueryHandler : BaseVehicleHandler<GetVehicleByIdQuery, Vehicle>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetVehicleByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="vehicleRepository">The vehicle repository for handling data operations.</param>
        public GetVehicleByIdQueryHandler(IVehicleRepository vehicleRepository) : base(vehicleRepository)
        {
        }

        /// <summary>
        /// Handles the execution of the GetVehicleByIdQuery by retrieving the vehicle from the repository.
        /// </summary>
        /// <param name="request">The GetVehicleByIdQuery containing the vehicle's unique identifier.</param>
        /// <param name="token">Cancellation token for the async operation.</param>
        /// <returns>The vehicle with the specified ID.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the vehicle is not found in the repository.</exception>
        public override async Task<Vehicle> Handle(GetVehicleByIdQuery request, CancellationToken token)
        {
            var vehicle = await ValidateVehicleExists(request.Id);
            return vehicle;
        }
    }
}
