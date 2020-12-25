using lanews.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lanews.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public UserService(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IdentityResult> RegisterUser(string FirstName, string LastName, string Email, string password, IEnumerable<string> Roles)
        {
            User user = new User() { FisrtName = FirstName, LastName = LastName, Email = Email, UserName = Email, RegistrationDate = DateTime.Now };
            var result = await _userManager.CreateAsync(user, password);
            
            if (result.Succeeded)
            {
                user = await _userManager.FindByNameAsync(user.UserName);

                foreach (var role in Roles)
                {
                    if (await _roleManager.RoleExistsAsync(role))
                    {
                        result = await _userManager.AddToRoleAsync(user, role);
                    }
                }
            }
            return result;
        }
    }
}
