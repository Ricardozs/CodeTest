using MediatR;
using CodeTestApi.Domain.Interfaces;
using CodeTestApi.Domain.Entities;
using CodeTestApi.Application.Base_Handlers;

namespace CodeTestApi.Application.Queries.Vehicles
{
    /// <summary>
    /// Query to retrieve available vehicles within a specified date range.
    /// </summary>
    public class GetAvailableVehiclesQuery : IRequest<IEnumerable<Vehicle>>
    {
        /// <summary>
        /// The start date of the rental period.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// The end date of the rental period.
        /// </summary>
        public DateTime EndDate { get; set; }
    }

    /// <summary>
    /// Handler for processing the retrieval of available vehicles within a date range.
    /// </summary>
    public class GetAvailableVehiclesQueryHandler : BaseVehicleHandler<GetAvailableVehiclesQuery, IEnumerable<Vehicle>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAvailableVehiclesQueryHandler"/> class.
        /// </summary>
        /// <param name="vehicleRepository">The vehicle repository for handling data operations.</param>
        public GetAvailableVehiclesQueryHandler(IVehicleRepository vehicleRepository, IVehicleDomainService vehicleDomainService) : base(vehicleRepository, vehicleDomainService)
        {
        }

        /// <summary>
        /// Handles the execution of the GetAvailableVehiclesQuery by retrieving available vehicles
        /// within the specified date range from the repository.
        /// </summary>
        /// <param name="request">The GetAvailableVehiclesQuery containing the start and end dates for the availability check.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>An IEnumerable of available vehicles within the specified date range.</returns>
        public override async Task<IEnumerable<Vehicle>> Handle(GetAvailableVehiclesQuery request, CancellationToken cancellationToken)
        {
            return await _vehicleRepository.GetAllAvailableVehiclesAsync(request.StartDate, request.EndDate);
        }
    }
}
