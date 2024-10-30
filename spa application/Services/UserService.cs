using Destinationosh.Models;
using Microsoft.EntityFrameworkCore;

namespace Destinationosh.Services;

public class UserService: IUserService
{
    private ApplicationContext db = null!;
    public UserService(ApplicationContext db)
    {
        this.db = db;
    }

    public async Task AddUser(User user)
    {
        await db.Users.AddAsync(user);
        await db.SaveChangesAsync();
    }

    public async Task<User[]> GetUsersAsync()
    {
        return await db.Users.ToArrayAsync();
    }

    public async Task RemoveUser(User user)
    {
        db.Users.Remove(user);
        await db.SaveChangesAsync();
    }
}