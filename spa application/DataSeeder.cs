
using Destinationosh.Models;
using Microsoft.AspNetCore.Identity;

public static class DataSeeder 
{
    public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        // Создание ролей
        foreach(var role in Enum.GetNames<UserRole>())
        {
            if(!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
        // Создание пользователей
        if (await userManager.FindByNameAsync("admin") == null)
        {
            var user = new User
            {
                UserName = "admin",
                FullName = "Администратор",
                Role = UserRole.Admin
            };
            var result = await userManager.CreateAsync(user, "Admin@123");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "admin");
            }
        }
        if (await userManager.FindByNameAsync("editor") == null)
        {
            var user = new User
            {
                UserName = "editor",
                FullName = "Пользователь",
                Role = UserRole.Editor
            };
            var result = await userManager.CreateAsync(user, "Editor@123");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Editor");
            }
        }
    }
}