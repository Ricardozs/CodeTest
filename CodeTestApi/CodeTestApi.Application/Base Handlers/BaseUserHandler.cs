using CodeTestApi.Domain.Interfaces;
using MediatR;

namespace CodeTestApi.Application.Base_Handlers
{
    public class BaseUserHandler<T, Y> : IRequestHandler<T, Y> where T : IRequest<Y>
    {
        protected readonly IUserRepository _userRepository;
        protected BaseUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public virtual Task<Y> Handle(T request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
