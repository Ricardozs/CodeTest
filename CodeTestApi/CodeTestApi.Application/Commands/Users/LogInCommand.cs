using MediatR;
using CodeTestApi.Domain.Interfaces;
using System.Security.Claims;
using System.Text;
using CodeTestApi.Application.Base_Handlers;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace CodeTestApi.Application.Commands.Users
{
    public class LogInCommand : IRequest<string>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

    public class LogInHandler : BaseUserHandler<LogInCommand, string>
    {
        public LogInHandler(IUserRepository userRepository): base(userRepository)
        {
            
        }

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
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.UserType.ToString())
            }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
            return "";
        }
    }
}
