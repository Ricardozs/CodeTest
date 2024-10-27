using CodeTestApi.Application.Commands.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeTestApi.Host.Controllers
{
    /// <summary>
    /// Controller for handling authentication-related requests.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : RentalApiControllerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator instance for handling requests.</param>
        public AuthController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Logs in a user and generates a JWT token.
        /// </summary>
        /// <param name="command">The LogInCommand containing the user's login details.</param>
        /// <returns>An IActionResult containing the JWT token if successful, or Unauthorized if the login fails.</returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LogInCommand command)
        {
            try
            {
                var token = await _mediator.Send(command);
                return Ok(token);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }
    }
}
