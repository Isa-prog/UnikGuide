using Destinationosh.Models;
using Microsoft.EntityFrameworkCore;

namespace Destinationosh.Services;

public class PostService : IPostService
{
    private ApplicationContext db = null!;
    public PostService(ApplicationContext db)
    {
        this.db = db;
    }

    public async Task AddPost(Post post)
    {
        await db.Posts.AddAsync(post);
        await db.SaveChangesAsync();;
    }

    public async Task<Post[]> GetPosts()
    {
        return await db.Posts.ToArrayAsync();
    }

    public async Task<Post> GetPost(int id)
    {
        return await db.Posts.FirstAsync(p => p.Id == id);
    }

    public async Task RemovePost(Post post)
    {
        db.Posts.Remove(post);
        await db.SaveChangesAsync();
    }

    public async Task SavePost(Post post)
    {
        db.Posts.Update(post);
        db.Entry(post).State = EntityState.Modified;
        await db.SaveChangesAsync();
    }
}