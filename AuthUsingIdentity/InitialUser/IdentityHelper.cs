using AuthUsingIdentity.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthUsingIdentity.InitialUser
{
    public class IdentityHelper
    {
        internal static void SeedIdentites(ApplicationDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!roleManager.RoleExists(InitialRole.ROLE_ADMINISTRATOR))
            {
                var roleResult = roleManager.Create(new IdentityRole(InitialRole.ROLE_ADMINISTRATOR));
                if (roleResult.Succeeded)
                {
                    string UserName = "shakil@gmail.com";
                    string password = "@S003o";
                    ApplicationUser user = userManager.FindByName(UserName);
                    if (user==null)
                    {
                        user = new ApplicationUser()
                        {
                            UserName = UserName,
                            Email = UserName,
                            EmailConfirmed = true
                        };
                        IdentityResult identityResult = userManager.Create(user, password);
                        if (identityResult.Succeeded)
                        {
                            var result = userManager.AddToRole(user.Id, InitialRole.ROLE_ADMINISTRATOR);
                        }
                    }

                }
            }
        }
    }
}