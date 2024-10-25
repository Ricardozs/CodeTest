using CodeTestApi.Application.Base_Handlers;
using CodeTestApi.Domain.Interfaces;
using MediatR;

namespace CodeTestApi.Application.Commands.Users
{
    public class DeleteUserCommand : IRequest<Unit>
    {
        public string Id { get; set; }
        public DeleteUserCommand(string id)
        {
            Id = id;
        }
    }

    public class DeleteUserHandler : BaseUserHandler<DeleteUserCommand, Unit>
    {
        public DeleteUserHandler(IUserRepository userRepository): base(userRepository)
        {
            
        }

        public override async Task<Unit> Handle(DeleteUserCommand request,  CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdAsync(request.Id);
            if (user is null)
            {
                throw new KeyNotFoundException();
            }

            await _userRepository.DeleteUserAsync(request.Id);

            return Unit.Value;
        }
    }
}
