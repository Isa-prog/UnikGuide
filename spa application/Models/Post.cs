using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Destinationosh.Models;

public class Post
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    public string Ru { get; set; }
    public string En { get; set; }
    public string Kg { get; set; }
    public string UserId { get; set; }
    public virtual User? User { get; set; }
    public int RouteId { get; set; }
    public RouteModel Route { get; set; } = null;

}
