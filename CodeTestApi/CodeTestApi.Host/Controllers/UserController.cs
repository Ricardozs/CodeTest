using CodeTestApi.Application.Commands.Users;
using CodeTestApi.Application.Queries.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeTestApi.Host.Controllers
{
    /// <summary>
    /// Controller for managing user-related operations.
    /// Requires Admin role for access.
    /// </summary>
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : RentalApiControllerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator instance for handling requests.</param>
        public UserController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="command">The CreateUserCommand containing user details.</param>
        /// <returns>A CreatedAtAction result containing the ID of the newly created user.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            var userId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetUserById), new { id = userId }, userId);
        }

        /// <summary>
        /// Creates a new customer user (non-admin).
        /// </summary>
        /// <param name="command">The CreateUserCustomerCommand containing customer details.</param>
        /// <returns>A CreatedAtAction result containing the ID of the newly created customer.</returns>
        [HttpPost("registry")]
        public async Task<IActionResult> CreateUserCustomer([FromBody] CreateUserCustomerCommand command)
        {
            var userId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetUserById), new { id = userId }, userId);
        }

        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns>An Ok result containing the user's details or a NotFound result if the user is not found.</returns>
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

        /// <summary>
        /// Retrieves all users in the system.
        /// </summary>
        /// <returns>An Ok result containing all users.</returns>
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _mediator.Send(new GetUsersQuery());
            return Ok(users);
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="command">The UpdateUserCommand containing updated user details.</param>
        /// <returns>A NoContent result if the update is successful, or a NotFound result if the user is not found.</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command)
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

        /// <summary>
        /// Deletes a user by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user to delete.</param>
        /// <returns>A NoContent result if the deletion is successful, or a NotFound result if the user is not found.</returns>
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
    }
}
