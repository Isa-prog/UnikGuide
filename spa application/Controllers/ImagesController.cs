using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Destinationosh.Controllers;

[Route("[controller]")]
[Authorize]
public class ImageController : ControllerBase
{
    private readonly IWebHostEnvironment _environment;

    public ImageController(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    [HttpGet("name")]
    public IActionResult GetName()
    {
        return Ok(_environment.WebRootPath);
    }

    [HttpPost("upload")]
    public IActionResult Image(IFormFile file)
    {
        if(file == null)
        {
            return BadRequest("File is null");
        }
        if(file.Length > 1024 * 1024 * 2)
        {
            return BadRequest("File size is too big");
        }
        try
        {
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            using (var stream = new FileStream(Path.Combine(_environment.WebRootPath, fileName), FileMode.Create))
            {
                // Save the file
                file.CopyTo(stream);

                // Return the URL of the file
                var url = Url.Content($"~/{fileName}");

                return Ok(new { Success="1", Url = url, File = new { Url = url} });
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    [HttpPost("uploadForm")]
    public IActionResult UploadImage(FormFileCollection form)
    {
        return Image(form["image"]);
    }


}
