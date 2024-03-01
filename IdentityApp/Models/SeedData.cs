using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityApp.Models;

public static class SeedData
{
    public const string  adminUser = "Admin";
    public const string adminPassword = "Admin_123";

    public static async void IdentityTestUser (IApplicationBuilder app) 
    {
        var context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<DataContext>();

        if(context.Database.GetAppliedMigrations().Any())
        {
            context.Database.Migrate();
        }

        var userManager = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<UserManager<AppUser>>();

        var user = await userManager.FindByNameAsync(adminUser);

        if(user == null)
        {
            user = new AppUser 
            {
                FullName = "Batuhan Toy" ,
                UserName = adminUser ,
                Email = "admin@batutoy.com" ,
                PhoneNumber = "121212"
            };

            await userManager.CreateAsync(user , adminPassword);
        }
    }
}