using CodeTestApi.Application.Base_Handlers;
using CodeTestApi.Domain.Entities;
using CodeTestApi.Domain.Interfaces;
using MediatR;

namespace CodeTestApi.Application.Queries.Users
{
    /// <summary>
    /// Query to retrieve all users in the system.
    /// </summary>
    public class GetUsersQuery : IRequest<IEnumerable<User>>
    {
        // No parameters needed for retrieving all users.
    }

    /// <summary>
    /// Handler for processing the retrieval of all users.
    /// </summary>
    public class GetUsersQueryHandler : BaseUserHandler<GetUsersQuery, IEnumerable<User>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetUsersQueryHandler"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository for handling data operations.</param>
        public GetUsersQueryHandler(IUserRepository userRepository) : base(userRepository)
        {
        }

        /// <summary>
        /// Handles the execution of the GetUsersQuery by retrieving all users from the repository.
        /// </summary>
        /// <param name="request">The GetUsersQuery, which does not require any parameters.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>An IEnumerable of all users in the system.</returns>
        public override async Task<IEnumerable<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetAllUsersAsync();
        }
    }
}
