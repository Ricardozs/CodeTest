using CodeTestApi.Application.Base_Handlers;
using CodeTestApi.Domain.Enums;
using CodeTestApi.Domain.Interfaces;
using CodeTestApi.Domain.Entities;
using MediatR;

namespace CodeTestApi.Application.Commands.Users
{
    public class CreateUserCommand : IRequest<string>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string HashPassword { get; set; }
        public UserType UserType { get; set; }
    }

    public class CreateUserHandler : BaseUserHandler<CreateUserCommand, string>
    {
        public CreateUserHandler(IUserRepository userRepository): base(userRepository)
        {
        }

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
