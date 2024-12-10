using CodeTestApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CodeTestApi.Domain.Interfaces
{
    public interface IUserDomainService
    {
        Task<User> GetUserOrThrowAsync(string userId);
        Task<ClaimsIdentity> ValidateUserCredentialsAsync(string email, string password);
    }
}
