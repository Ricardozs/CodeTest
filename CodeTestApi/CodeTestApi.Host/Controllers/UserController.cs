using CodeTestApi.Application.Commands.Users;
using CodeTestApi.Application.Queries.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeTestApi.Host.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            var userId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetUserById), new { id = userId }, userId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {

                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                await _mediator.Send(new DeleteUserCommand(id));
                return NoContent();
            }
            catch (KeyNotFoundException)
            {

                return NotFound();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            try
            {
                var user = await _mediator.Send(new GetUserByIdQuery(id));
                return Ok(user);
            }
            catch (KeyNotFoundException)
            {

                return NotFound();
            }
        }
    }

}
