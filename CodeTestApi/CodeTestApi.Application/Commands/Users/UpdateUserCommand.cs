using CodeTestApi.Application.Base_Handlers;
using CodeTestApi.Domain.Enums;
using CodeTestApi.Domain.Interfaces;
using MediatR;

namespace CodeTestApi.Application.Commands.Users
{
    public class UpdateUserCommand : IRequest<Unit>
    {
        public required string Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string HashPassword { get; set; }
        public UserType UserType { get; set; }
    }

    public class UpdateUserHandler : BaseUserHandler<UpdateUserCommand, Unit>
    {
        public UpdateUserHandler(IUserRepository userRepository): base(userRepository)
        {
            
        }

        public override async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdAsync(request.Id);
            if(user is null)
            {
                throw new KeyNotFoundException();
            }

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
