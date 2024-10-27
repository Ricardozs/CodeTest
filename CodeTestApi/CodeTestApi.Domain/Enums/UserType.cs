using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTestApi.Domain.Enums
{
    /// <summary>
    /// Enum representing the types of user.
    /// </summary>
    public enum UserType
    {
        /// <summary>
        /// User type with most privileges.
        /// </summary>
        Admin,

        /// <summary>
        /// User type for the customers.
        /// </summary>
        User
    }
}
