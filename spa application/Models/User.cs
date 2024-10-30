using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Destinationosh.Models;

public class User : IdentityUser
{
    public string FullName { get; set; } = null!;
    [NotMapped]
    public string Password { get; set; } = null!;
    public UserRole Role { get; set; }
    public List<Post> Posts { get; set; } = new();
}
