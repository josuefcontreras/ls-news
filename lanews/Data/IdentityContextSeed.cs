using lanews.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lanews.Data
{
    public class IdentityContextSeed
    {
        private const string ADMINISTRATOR = "Administrator";
        private const string EDITOR = "Editor";
        private const string READER = "Reader";
        private const string DEFAULT_PASSWORD = "123Mami123$";
        public async static Task SeedAsync(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            // Create Roles
            await roleManager.CreateAsync(new Role() { Name = ADMINISTRATOR});
            await roleManager.CreateAsync(new Role() { Name = EDITOR });
            await roleManager.CreateAsync(new Role() { Name = READER });

            // Add demo admin
            var demoAdmin  = new User { UserName = "demoadmin@example.com", Email = "demoadmin@example.com", FisrtName = "Juan A.", LastName= "Perez", EmailConfirmed=true, RegistrationDate = DateTime.Now };
            await userManager.CreateAsync(demoAdmin, DEFAULT_PASSWORD);
            demoAdmin = await userManager.FindByNameAsync(demoAdmin.UserName);
            await userManager.AddToRolesAsync(demoAdmin, new List<string>() { ADMINISTRATOR, EDITOR, READER });

            // Add demo editor
            var demoEditor = new User { UserName = "demoeditor@example.com", Email = "demoeditorn@example.com", FisrtName = "Juan E.", LastName = "Perez", EmailConfirmed = true, RegistrationDate = DateTime.Now };
            await userManager.CreateAsync(demoEditor, DEFAULT_PASSWORD);
            demoEditor = await userManager.FindByNameAsync(demoEditor.UserName);
            await userManager.AddToRolesAsync(demoEditor, new List<string>() {EDITOR, READER });

            // Add demo reader
            var demoReader = new User { UserName = "demoreader@example.com", Email = "demoreader@example.com", FisrtName = "Juan R.", LastName = "Perez", EmailConfirmed = true, RegistrationDate = DateTime.Now };
            await userManager.CreateAsync(demoReader, DEFAULT_PASSWORD);
            demoReader = await userManager.FindByNameAsync(demoReader.UserName);
            await userManager.AddToRoleAsync(demoReader, READER);

        }
    }
}
