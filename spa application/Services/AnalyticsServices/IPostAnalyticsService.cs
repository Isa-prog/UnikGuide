using Destinationosh.Models;

namespace Destinationosh.Services;

public interface IPostAnalyticsService
{
    Task<PostVisit[]> GetPostVisits(Post post);
    Task AddPostVisit(Post post);
    Task<PostAnalytics> GetPostAnalytics(Post post);
}