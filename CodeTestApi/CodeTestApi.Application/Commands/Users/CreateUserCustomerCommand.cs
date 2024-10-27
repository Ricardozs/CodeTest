using CodeTestApi.Application.Base_Handlers;
using CodeTestApi.Domain.Entities;
using CodeTestApi.Domain.Enums;
using CodeTestApi.Domain.Interfaces;
using MediatR;

namespace CodeTestApi.Application.Commands.Users
{
    /// <summary>
    /// Command to create a new customer user in the system.
    /// </summary>
    public class CreateUserCustomerCommand : IRequest<string>
    {
        /// <summary>
        /// The customer's first name.
        /// </summary>
        public required string FirstName { get; set; }

        /// <summary>
        /// The customer's last name.
        /// </summary>
        public required string LastName { get; set; }

        /// <summary>
        /// The customer's email address.
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// The customer's phone number.
        /// </summary>
        public required string Phone { get; set; }

        /// <summary>
        /// The password of the customer.
        /// </summary>
        public required string Password { get; set; }
    }

    /// <summary>
    /// Handler for creating a new customer user in the system.
    /// </summary>
    public class CreateUserCustomerHandler : BaseUserHandler<CreateUserCustomerCommand, string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserCustomerHandler"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository for handling data operations.</param>
        public CreateUserCustomerHandler(IUserRepository userRepository) : base(userRepository)
        {
        }

        /// <summary>
        /// Handles the execution of the CreateUserCustomerCommand by adding a new customer user to the repository.
        /// </summary>
        /// <param name="request">The CreateUserCustomerCommand containing the customer's information.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>The unique identifier (ID) of the newly created customer user.</returns>
        public override async Task<string> Handle(CreateUserCustomerCommand request, CancellationToken cancellationToken)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                HashPassword = hashedPassword,
                UserType = UserType.User,  // Sets the user type as 'User'
            };

            await _userRepository.AddUserAsync(user);

            return user.Id;
        }
    }
}
