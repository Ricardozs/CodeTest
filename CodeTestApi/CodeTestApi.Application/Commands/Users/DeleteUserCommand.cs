using CodeTestApi.Application.Base_Handlers;
using CodeTestApi.Domain.Interfaces;
using MediatR;

namespace CodeTestApi.Application.Commands.Users
{
    /// <summary>
    /// Command to delete a user from the system by their unique identifier.
    /// </summary>
    public class DeleteUserCommand : IRequest<Unit>
    {
        /// <summary>
        /// The unique identifier of the user to be deleted.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteUserCommand"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the user to be deleted.</param>
        public DeleteUserCommand(string id)
        {
            Id = id;
        }
    }

    /// <summary>
    /// Handler for deleting a user from the system.
    /// </summary>
    public class DeleteUserHandler : BaseUserHandler<DeleteUserCommand, Unit>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteUserHandler"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository for handling data operations.</param>
        public DeleteUserHandler(IUserRepository userRepository) : base(userRepository)
        {
        }

        /// <summary>
        /// Handles the execution of the DeleteUserCommand by deleting the user from the repository.
        /// </summary>
        /// <param name="request">The DeleteUserCommand containing the user's unique identifier.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>An empty Unit value upon successful deletion.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the user is not found.</exception>
        public override async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
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
