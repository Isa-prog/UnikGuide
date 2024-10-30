using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Destionationosh.Controllers;

[Route("[controller]")]
[Authorize]
public class PostController: ControllerBase
{
    
}