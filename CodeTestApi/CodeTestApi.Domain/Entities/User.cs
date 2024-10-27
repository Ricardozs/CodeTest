using CodeTestApi.Domain.Enums;

namespace CodeTestApi.Domain.Entities
{
    /// <summary>
    /// Entity representing an user.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the user's unique identifier.
        /// </summary>
        public required string Id { get; set; }

        /// <summary>
        /// Gets or sets the user's first name.
        /// </summary>
        public required string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the user's last name.
        /// </summary>
        public required string LastName { get; set; }

        /// <summary>
        /// Gets or sets the user's email.
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// Gets or sets the user's phone.
        /// </summary>
        public required string Phone { get; set; }

        /// <summary>
        /// Gets or sets the user's hash password.
        /// </summary>
        public required string HashPassword { get; set; }

        /// <summary>
        /// Gets or sets the user's user type.
        /// </summary>
        public UserType UserType { get; set; }
    }
}
