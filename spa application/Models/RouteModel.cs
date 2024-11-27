namespace Destinationosh.Models;

public class RouteModel
{
    public int Id { get; set;}
    public string Name { get; set; } = null!;
    public int ParrentId { get; set; }
    public virtual Post? Post { get; set; }
    public virtual RouteModel[] ChildrenRoutes { get; set; }
    public virtual RouteModel ParentRoute { get; set; }
}