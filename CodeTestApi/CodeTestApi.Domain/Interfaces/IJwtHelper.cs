using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CodeTestApi.Domain.Interfaces
{
    public interface IJwtHelper
    {
        string GenerateToken(ClaimsIdentity claimsIdentity, DateTime expiration);
    }
}
