using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lanews.Services
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterUser(string FirstName, string LastName, string Email, string Password, IEnumerable<string> Roles);
    }
}
