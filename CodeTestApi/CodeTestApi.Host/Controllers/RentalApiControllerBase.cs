using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CodeTestApi.Host.Controllers
{
    /// <summary>
    /// Base controller for API endpoints in the rental system.
    /// Provides access to the Mediator for handling commands and queries.
    /// </summary>
    public class RentalApiControllerBase : ControllerBase
    {
        /// <summary>
        /// The Mediator for sending commands and queries.
        /// </summary>
        protected readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="RentalApiControllerBase"/> class.
        /// </summary>
        /// <param name="mediator">The mediator instance for handling requests.</param>
        public RentalApiControllerBase(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
