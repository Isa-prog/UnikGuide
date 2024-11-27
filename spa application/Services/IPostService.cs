using Destinationosh.Models;

namespace Destinationosh.Services;

public interface IPostService
{
    Task<Post[]> GetPosts();
    Task<Post> GetPost(int id);
    Task RemovePost(Post post);
    Task AddPost(Post post);
    Task SavePost(Post post);
}