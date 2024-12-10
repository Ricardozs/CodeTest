using CodeTestApi.Application.Base_Handlers;
using CodeTestApi.Domain.Enums;
using CodeTestApi.Domain.Interfaces;
using CodeTestApi.Domain.Entities;
using MediatR;

namespace CodeTestApi.Application.Commands.Users
{
    /// <summary>
    /// Command to create a new user in the system.
    /// </summary>
    public class CreateUserCommand : IRequest<string>
    {
        /// <summary>
        /// The user's first name.
        /// </summary>
        public required string FirstName { get; set; }

        /// <summary>
        /// The user's last name.
        /// </summary>
        public required string LastName { get; set; }

        /// <summary>
        /// The user's email address.
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// The user's phone number.
        /// </summary>
        public required string Phone { get; set; }

        /// <summary>
        /// The hashed password of the user.
        /// </summary>
        public required string HashPassword { get; set; }

        /// <summary>
        /// The type of user being created (Admin or User).
        /// </summary>
        public UserType UserType { get; set; }
    }

    /// <summary>
    /// Handler for creating a new user in the system.
    /// </summary>
    public class CreateUserHandler : BaseUserHandler<CreateUserCommand, string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserHandler"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository for handling data operations.</param>
        public CreateUserHandler(IUserRepository userRepository, IUserDomainService userDomainService) : base(userRepository, userDomainService)
        {
        }

        /// <summary>
        /// Handles the execution of the CreateUserCommand by adding a new user to the repository.
        /// </summary>
        /// <param name="request">The CreateUserCommand containing the user's information.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>The unique identifier (ID) of the newly created user.</returns>
        public override async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.HashPassword);

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                HashPassword = hashedPassword,
                UserType = request.UserType,
            };

            await _userRepository.AddUserAsync(user);

            return user.Id;
        }
    }
}
