using CodeTestApi.Domain.Enums;

namespace CodeTestApi.Domain.Entities
{
    public class User
    {
        public required string Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string HashPassword { get; set; }
        public UserType UserType { get; set; }
    }
}
