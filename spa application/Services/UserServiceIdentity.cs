using Destinationosh.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Destinationosh.Services;

public class UserServiceIdentity: IUserService
{
    private readonly UserManager<User> _userManager;
    public UserServiceIdentity(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task AddUser(User user)
    {
        var addUserTask =  await _userManager.CreateAsync(user, user.Password);
        var addRoleTask =  await _userManager.AddToRoleAsync(user, user.Role.ToString());
        if(!addRoleTask.Succeeded && addUserTask.Succeeded)
        {
            await _userManager.DeleteAsync(user);
        }
        if(!addUserTask.Succeeded || !addRoleTask.Succeeded)
        {
            throw new Exception("Ошибка при создании пользователя");
        }
    }

    public async Task<User[]> GetUsersAsync()
    {
        return await _userManager.Users.ToArrayAsync();
    }

    public async Task RemoveUser(User user)
    {
        await _userManager.DeleteAsync(user);
    }
}