using MediatR;
using CodeTestApi.Domain.Entities;
using CodeTestApi.Domain.Interfaces;
using CodeTestApi.Application.Base_Handlers;

namespace CodeTestApi.Application.Queries.Users
{
    public class GetUserByIdQuery : IRequest<User>
    {
        public string Id { get; set; }
        public GetUserByIdQuery(string id)
        {
            Id = id;
        }
    }
    public class GetUserByIdQueryHandler : BaseUserHandler<GetUserByIdQuery, User>
    {
        public GetUserByIdQueryHandler(IUserRepository userRepository) : base(userRepository)
        {
        }

        public override async Task<User> Handle(GetUserByIdQuery request, CancellationToken token)
        {
            var user = await _userRepository.GetUserByIdAsync(request.Id);
            if (user is null)
            {
                throw new KeyNotFoundException();
            }
            return user;
        }
    }
}
