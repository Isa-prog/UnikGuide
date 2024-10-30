using Destinationosh.Models;

namespace Destinationosh.Services;

public class PostContainerService: IPostContainerService
{
    public IList<Post> Posts { get; } = new List<Post>();
}