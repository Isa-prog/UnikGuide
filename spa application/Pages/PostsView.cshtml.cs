using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Destinationosh.Pages;

public class PostsViewPageModel : PageModel
{
    public string Route { get; set; } = "";

    public void OnGet()
    {
        var path = HttpContext.Request.Path.Value;
        Route = path.Substring(path.IndexOf("/",1)+1);
    }
}