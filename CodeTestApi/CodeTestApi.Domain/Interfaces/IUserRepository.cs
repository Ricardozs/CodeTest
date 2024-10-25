using CodeTestApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTestApi.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(string id);
        Task<User> GetUserByEmailAsync(string email);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(string id);
    }

}
