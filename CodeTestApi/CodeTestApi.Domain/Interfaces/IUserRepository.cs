using CodeTestApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTestApi.Domain.Interfaces
{
    /// <summary>
    /// Repository interface for managing user-related data operations.
    /// Provides methods to retrieve, add, update, and delete users in the system.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Retrieves a user by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns>The user with the specified id, or null if no user is found.</returns>
        Task<User> GetUserByIdAsync(string id);

        /// <summary>
        /// Retrieves a user by its email address.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        /// <returns>The user with the specified email, or null if no user is found.</returns>
        Task<User> GetUserByEmailAsync(string email);

        /// <summary>
        /// Retrieves all users in the system.
        /// </summary>
        /// <returns>An IEnumerable of all users.</returns>
        Task<IEnumerable<User>> GetAllUsersAsync();

        /// <summary>
        /// Adds a new user to the system.
        /// </summary>
        /// <param name="user">The user entity to be added.</param>
        Task AddUserAsync(User user);

        /// <summary>
        /// Updates an existing user record in the system.
        /// </summary>
        /// <param name="user">The user entity with updated information.</param>
        Task UpdateUserAsync(User user);

        /// <summary>
        /// Deletes an existing user from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the user to be deleted.</param>
        Task DeleteUserAsync(string id);
    }
}
