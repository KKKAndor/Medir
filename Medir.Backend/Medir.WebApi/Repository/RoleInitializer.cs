using Medir.WebApi.Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace Medir.WebApi.Repository
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "adminnn@gmail.com";
            string password = "aDMIN123456";

            if (await roleManager.FindByNameAsync("Administrator") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Administrator"));
            }
            if (await roleManager.FindByNameAsync("Patient") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Patient"));
            }
            if (await roleManager.FindByNameAsync("Medic") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Medic"));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                    Address = "Hospital",
                    DateOfBirth = new DateTime(1999, 1, 1),
                    FirstName = "Admin1",
                    LastName = "Admin2",
                    Patronymic = "Admin3",
                    PhoneNumber = "77718821",
                    Gender = "male",
                    Registered = true
                };

                var result = await userManager.CreateAsync(admin, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Administrator");
                }
            }
        }
    }
}
