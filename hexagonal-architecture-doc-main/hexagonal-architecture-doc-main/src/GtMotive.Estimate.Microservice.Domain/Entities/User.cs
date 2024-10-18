using System;
using GtMotive.Estimate.Microservice.Domain.Enums;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <summary>
    /// Entity representing an user.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the users unique identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the users first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the users last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the users email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the user type.
        /// </summary>
        public UserType UserType { get; set; }
    }
}
