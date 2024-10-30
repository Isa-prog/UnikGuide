
namespace Destinationosh.Models;

public class PostVisit
{
    public int Id { get; set; }
    public string? UrlReferrer { get; set; }
    public string? Ip { get; set;}
    public DateTime VisitDate { get; set; }
    public int PostId { get; set; }
    public virtual Post Post { get; set; }
}