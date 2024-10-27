using MediatR;
using CodeTestApi.Domain.Entities;
using CodeTestApi.Domain.Interfaces;
using CodeTestApi.Application.Base_Handlers;

namespace CodeTestApi.Application.Queries.Users
{
    /// <summary>
    /// Query to retrieve a user by their unique identifier.
    /// </summary>
    public class GetUserByIdQuery : IRequest<User>
    {
        /// <summary>
        /// The unique identifier of the user to retrieve.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserByIdQuery"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the user to retrieve.</param>
        public GetUserByIdQuery(string id)
        {
            Id = id;
        }
    }

    /// <summary>
    /// Handler for processing the retrieval of a user by their unique identifier.
    /// </summary>
    public class GetUserByIdQueryHandler : BaseUserHandler<GetUserByIdQuery, User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository for handling data operations.</param>
        public GetUserByIdQueryHandler(IUserRepository userRepository) : base(userRepository)
        {
        }

        /// <summary>
        /// Handles the execution of the GetUserByIdQuery by retrieving the user from the repository.
        /// </summary>
        /// <param name="request">The GetUserByIdQuery containing the user's unique identifier.</param>
        /// <param name="token">Cancellation token for the async operation.</param>
        /// <returns>The user with the specified ID, or throws a KeyNotFoundException if the user is not found.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the user is not found in the repository.</exception>
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
