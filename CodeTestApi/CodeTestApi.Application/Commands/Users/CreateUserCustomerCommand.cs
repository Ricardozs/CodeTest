using CodeTestApi.Application.Base_Handlers;
using CodeTestApi.Domain.Entities;
using CodeTestApi.Domain.Enums;
using CodeTestApi.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTestApi.Application.Commands.Users
{
    public class CreateUserCustomerCommand : IRequest<string>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string HashPassword { get; set; }
    }

    public class CreateUserCustomerHandler : BaseUserHandler<CreateUserCustomerCommand, string>
    {
        public CreateUserCustomerHandler(IUserRepository userRepository) : base(userRepository)
        {
            
        }

        public override async Task<string> Handle(CreateUserCustomerCommand request, CancellationToken cancellationToken)
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
                UserType = UserType.User,
            };

            await _userRepository.AddUserAsync(user);

            return user.Id;
        }
    }
}
