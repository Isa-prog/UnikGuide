using Microsoft.AspNetCore.Mvc;

namespace Destinationosh.Controllers;


[Route("[controller]")]
public class EditorController : ControllerBase
{
    private readonly IWebHostEnvironment _environment;
    private readonly ILogger<EditorController> _logger;

    public EditorController(IWebHostEnvironment environment, ILogger<EditorController> logger)
    {
        _environment = environment;
        _logger = logger;
    }

    [HttpPost("imageUpload")]
    public IActionResult ImageUpload(IFormFile image = null!)
    {
        if(image.Length > 1024 * 1024 * 2)
        {
            return BadRequest("Image size is too big");
        }
        else
        {
            try
            {
                var imageName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imgs", imageName);
                var url = HttpContext.Request.Scheme +"://"+ HttpContext.Request.Host +Url.Content($"~/imgs/{imageName}");

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    image.CopyTo(stream);

                    
                }
                return Ok(new { Success = 1, File = new { Url = url } });
            }
            catch(Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }
    }

    [HttpPost("fetchUrl")]
    public IActionResult ImageUrl(string url)
    {
        var thisUrl = HttpContext.Request.Scheme +"://"+ HttpContext.Request.Host + "/";
        _logger.LogInformation($"{url}");
        if(thisUrl.StartsWith(url))
        {
            return Ok(new { Success = 1, File = new { Url = url } });
        }
        else
        {
            return BadRequest("Invalid url");
        }
    }
}