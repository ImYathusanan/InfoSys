using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InfoSys.Entities
{
    public static class UserAndRolesSeedInitializer
    {
        public static async Task UserAndRoleSeed(UserManager<IdentityUser> userManger,
                                      RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { "Admin", "Manager", "Staff" };
            foreach(var role in roles)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    IdentityResult result = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            //Admin User Creation
            if(userManger.FindByEmailAsync("admin@gmail.com").Result == null)
            {
                var user = new IdentityUser
                {
                    Email = "admin@gmail.com",
                    UserName = "admin@gmail.com"
                };

                var identityResult = userManger.CreateAsync(user, "Password1").Result;
                if(identityResult.Succeeded)
                {
                    userManger.AddToRoleAsync(user, "Admin").Wait();
                }
            }

            //Manager role
            if (userManger.FindByEmailAsync("manger@gmail.com").Result == null)
            {
                var user = new IdentityUser
                {
                    Email = "manager@gmail.com",
                    UserName = "manager@gmail.com"
                };

                var identityResult = userManger.CreateAsync(user, "Password1").Result;
                if (identityResult.Succeeded)
                {
                    userManger.AddToRoleAsync(user, "Manager").Wait();
                }
            }

            //Staff User
            if (userManger.FindByEmailAsync("staff@gmail.com").Result == null)
            {
                var user = new IdentityUser
                {
                    Email = "staff@gmail.com",
                    UserName = "staff@gmail.com"
                };

                var identityResult = userManger.CreateAsync(user, "Password1").Result;
                if (identityResult.Succeeded)
                {
                    userManger.AddToRoleAsync(user, "Staff").Wait();
                }
            }

            //No role user
            if (userManger.FindByEmailAsync("norole@gmail.com").Result == null)
            {
                var user = new IdentityUser
                {
                    Email = "norole@gmail.com",
                    UserName = "norole@gmail.com"
                };
            }
        }
    }
}
