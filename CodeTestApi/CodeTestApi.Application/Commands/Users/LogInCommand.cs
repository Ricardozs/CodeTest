using MediatR;
using CodeTestApi.Domain.Interfaces;
using System.Security.Claims;
using System.Text;
using CodeTestApi.Application.Base_Handlers;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using CodeTestApi.Application.Helpers;

namespace CodeTestApi.Application.Commands.Users
{
    /// <summary>
    /// Command to log in a user and generate a JWT token.
    /// </summary>
    public class LogInCommand : IRequest<string>
    {
        /// <summary>
        /// The user's email address.
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// The user's password.
        /// </summary>
        public required string Password { get; set; }
    }

    /// <summary>
    /// Handler for logging in a user and generating a JWT token.
    /// </summary>
    public class LogInHandler : BaseUserHandler<LogInCommand, string>
    {
        private readonly IJwtHelper _jwtHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogInHandler"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository for handling data operations.</param>
        public LogInHandler(IUserRepository userRepository, IUserDomainService userDomainService, IJwtHelper jwtHelper) : base(userRepository, userDomainService)
        {
            _jwtHelper = jwtHelper;
        }

        /// <summary>
        /// Handles the log in process by verifying the user's credentials and generating a JWT token.
        /// </summary>
        /// <param name="request">The LogInCommand containing the user's email and password.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>A JWT token if the credentials are valid.</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown when the credentials are invalid.</exception>
        public override async Task<string> Handle(LogInCommand request, CancellationToken cancellationToken)
        {
            var userClaims = await _userDomainService.ValidateUserCredentialsAsync(request.Email, request.Password);

            return _jwtHelper.GenerateToken(userClaims, DateTime.UtcNow.AddHours(1));
        }
    }
}
