using CodeTestApi.Domain.Interfaces;
using MediatR;

namespace CodeTestApi.Application.Base_Handlers
{
    /// <summary>
    /// Base handler class for handling user-related commands and queries.
    /// Provides access to the user repository and enforces implementation of the request handler.
    /// </summary>
    /// <typeparam name="T">The type of the request, which must implement IRequest&lt;Y&gt;.</typeparam>
    /// <typeparam name="Y">The type of the response expected from the handler.</typeparam>
    public class BaseUserHandler<T, Y> : IRequestHandler<T, Y> where T : IRequest<Y>
    {
        /// <summary>
        /// The repository for handling user data operations.
        /// </summary>
        protected readonly IUserRepository _userRepository;

        protected readonly IUserDomainService _userDomainService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseUserHandler{T, Y}"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository for handling data operations.</param>
        protected BaseUserHandler(IUserRepository userRepository, IUserDomainService userDomainService)
        {
            _userRepository = userRepository;
            _userDomainService = userDomainService;
        }

        /// <summary>
        /// Handles the request. This method must be overridden by derived classes to implement the specific logic.
        /// </summary>
        /// <param name="request">The request object.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>The result of the operation as a task of type Y.</returns>
        /// <exception cref="NotImplementedException">Thrown when the method is not implemented in the derived class.</exception>
        public virtual Task<Y> Handle(T request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
