using Destinationosh.Models;
using Destinationosh.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Destinationosh.Pages;

public class PostPageModel: PageModel
{
    public Post Post { get; set; } 
    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }
    private readonly IPostService _postService;
    private readonly IPostAnalyticsService _analytics;

    public PostPageModel(IPostService postService, IPostAnalyticsService analytics)
    {
        _postService = postService;
        _analytics = analytics;
    }

    public async Task OnGetAsync()
    {
        Post = await _postService.GetPost(Id);
        await _analytics.AddPostVisit(Post);
    }
}