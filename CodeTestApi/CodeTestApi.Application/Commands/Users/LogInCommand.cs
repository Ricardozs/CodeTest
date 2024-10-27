using MediatR;
using CodeTestApi.Domain.Interfaces;
using System.Security.Claims;
using System.Text;
using CodeTestApi.Application.Base_Handlers;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

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
        /// <summary>
        /// Initializes a new instance of the <see cref="LogInHandler"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository for handling data operations.</param>
        public LogInHandler(IUserRepository userRepository) : base(userRepository)
        {
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
            var user = await _userRepository.GetUserByEmailAsync(request.Email);

            if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.HashPassword))
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("#MySuperSecretKeyThatNoOneWillGuessAndSuperLongEnough123456789!$");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.UserType.ToString())
                ]),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
