using System.Security.Claims;
using CodeTestApi.Domain.Entities;
using CodeTestApi.Domain.Interfaces;

namespace CodeTestApi.Application.Domain_Services
{
    public class UserDomainService : IUserDomainService
    {
        private readonly IUserRepository _userRepository;

        public UserDomainService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserOrThrowAsync(string userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user is null)
            {
                throw new KeyNotFoundException("User not found");
            }
            return user;
        }

        public async Task<ClaimsIdentity> ValidateUserCredentialsAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);

            if (user is null || !BCrypt.Net.BCrypt.Verify(password, user.HashPassword))
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            return new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.UserType.ToString())
            });
        }
    }
}
