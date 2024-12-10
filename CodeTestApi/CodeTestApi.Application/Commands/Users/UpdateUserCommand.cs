using CodeTestApi.Application.Base_Handlers;
using CodeTestApi.Domain.Enums;
using CodeTestApi.Domain.Interfaces;
using MediatR;

namespace CodeTestApi.Application.Commands.Users
{
    /// <summary>
    /// Command to update an existing user's details in the system.
    /// </summary>
    public class UpdateUserCommand : IRequest<Unit>
    {
        /// <summary>
        /// The unique identifier of the user to be updated.
        /// </summary>
        public required string Id { get; set; }

        /// <summary>
        /// The updated first name of the user.
        /// </summary>
        public required string FirstName { get; set; }

        /// <summary>
        /// The updated last name of the user.
        /// </summary>
        public required string LastName { get; set; }

        /// <summary>
        /// The updated email address of the user.
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// The updated phone number of the user.
        /// </summary>
        public required string Phone { get; set; }

        /// <summary>
        /// The updated hashed password of the user.
        /// </summary>
        public required string HashPassword { get; set; }

        /// <summary>
        /// The updated user type (Admin or User).
        /// </summary>
        public UserType UserType { get; set; }
    }

    /// <summary>
    /// Handler for updating an existing user's details in the system.
    /// </summary>
    public class UpdateUserHandler : BaseUserHandler<UpdateUserCommand, Unit>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUserHandler"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository for handling data operations.</param>
        public UpdateUserHandler(IUserRepository userRepository, IUserDomainService userDomainService) : base(userRepository, userDomainService)
        {
        }

        /// <summary>
        /// Handles the execution of the UpdateUserCommand by updating the user's details in the repository.
        /// </summary>
        /// <param name="request">The UpdateUserCommand containing the updated user details.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>An empty Unit value upon successful update.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the user is not found.</exception>
        public override async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userDomainService.GetUserOrThrowAsync(request.Id);

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.Phone = request.Phone;
            user.HashPassword = request.HashPassword;
            user.UserType = request.UserType;

            await _userRepository.UpdateUserAsync(user);

            return Unit.Value;
        }
    }
}
