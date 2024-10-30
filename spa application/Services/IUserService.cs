using Destinationosh.Models;

namespace Destinationosh.Services;

public interface IUserService
{
    Task<User[]> GetUsersAsync();
    Task AddUser(User user);
    Task RemoveUser(User user);
}