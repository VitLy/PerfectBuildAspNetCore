using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PerfectBuild.Models;
using System;
using System.Linq;

namespace PerfectBuild.Infrastructure
{
    /// <summary>
    /// Создание Superuser из конфигурационного файла
    /// </summary>
    internal static class IdentitySeedData
    {
        internal static async void EnsurePopulation(IApplicationBuilder app, IConfiguration configuration)
        {
            UserManager<User> userManager = app.ApplicationServices.GetRequiredService<UserManager<User>>();
            RoleManager<IdentityRole> roleManager = app.ApplicationServices.GetRequiredService<RoleManager<IdentityRole>>();

            string adminName = configuration.GetValue<string>("Data:AdminUser:Name");
            string adminRole = configuration.GetValue<string>("Data:AdminUser:Role");
            string adminEmail = configuration.GetValue<string>("Data:AdminUser:Email");
            string adminPassword = configuration.GetValue<string>("Data:AdminUser:Password");

            if (String.IsNullOrEmpty(adminName) || String.IsNullOrEmpty(adminPassword) || String.IsNullOrEmpty(adminRole))
            {
                return;
            }
            else
            {
                if (!userManager.Users.Any()) 
                {
                    bool roleIsExists = await roleManager.RoleExistsAsync(adminRole); 
                    if (!roleIsExists)
                    {
                        var addRoleResult = await roleManager.CreateAsync(new IdentityRole(adminRole));
                        if (!addRoleResult.Succeeded)
                        {
                            return;
                        }
                    }
                    User admin = new User { UserName = adminName,Email=adminEmail };
                    var addUserResult = await userManager.CreateAsync(admin, adminPassword);
                    if (addUserResult.Succeeded) 
                    {
                        await userManager.AddToRoleAsync(admin, adminRole);
                    }
                    else 
                    {
                        var errors = addUserResult.Errors;
                    }
                }
            }
        }
    }
}
