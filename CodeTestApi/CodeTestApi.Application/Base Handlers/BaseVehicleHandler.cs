using CodeTestApi.Domain.Entities;
using CodeTestApi.Domain.Interfaces;
using MediatR;

namespace CodeTestApi.Application.Base_Handlers
{
    /// <summary>
    /// Base handler class for handling vehicle-related commands and queries.
    /// Provides access to the vehicle repository and enforces implementation of the request handler.
    /// </summary>
    /// <typeparam name="T">The type of the request, which must implement IRequest&lt;Y&gt;.</typeparam>
    /// <typeparam name="Y">The type of the response expected from the handler.</typeparam>
    public abstract class BaseVehicleHandler<T, Y> : IRequestHandler<T, Y> where T : IRequest<Y>
    {
        /// <summary>
        /// The repository for handling vehicle data operations.
        /// </summary>
        protected readonly IVehicleRepository _vehicleRepository;

        protected readonly IVehicleDomainService _vehicleDomainService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseVehicleHandler{T, Y}"/> class.
        /// </summary>
        /// <param name="vehicleRepository">The vehicle repository for handling data operations.</param>
        protected BaseVehicleHandler(IVehicleRepository vehicleRepository, IVehicleDomainService vehicleDomainService)
        {
            _vehicleRepository = vehicleRepository;
            _vehicleDomainService = vehicleDomainService;
        }

        /// <summary>
        /// Handles the request. This method must be overridden by derived classes to implement the specific logic.
        /// </summary>
        /// <param name="request">The request object.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>The result of the operation as a task of type Y.</returns>
        /// <exception cref="NotImplementedException">Thrown when the method is not implemented in the derived class.</exception>
        public virtual Task<Y> Handle(T request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
