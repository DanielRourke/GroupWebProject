using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupWebProject.Data
{
    public class SeedRoles
    {
        public static async Task CreateRoles(IServiceProvider serviceProvider, IConfiguration Configuration)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            string[] roleNames = { "Admin", "Customers" };
        }


    }
}
