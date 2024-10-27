using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CodeTestApi.Host.Controllers
{
    public class RentalApiControllerBase : ControllerBase
    {
        protected readonly IMediator _mediator;
        public RentalApiControllerBase(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
