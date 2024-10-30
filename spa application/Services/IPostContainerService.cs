using Destinationosh.Models;

namespace Destinationosh.Services;

public interface IPostContainerService
{
    IList<Post> Posts { get; }
}