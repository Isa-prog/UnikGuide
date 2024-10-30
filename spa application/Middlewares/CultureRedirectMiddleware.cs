using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Options;

namespace Destinationosh.Middlewares;

public class CultureRedirectMiddleware
{
    private static readonly string[] supportedCultures = new[] { "en", "ru", "kg" };
    private readonly RequestDelegate _next;
    private readonly SupportedCultureOptions _supportedCultureOptions;
    private readonly ILogger<CultureRedirectMiddleware> _logger;

    public CultureRedirectMiddleware(RequestDelegate next, IOptions<SupportedCultureOptions> option, ILogger<CultureRedirectMiddleware> logger)
    {
        _next = next;
        _supportedCultureOptions = option.Value;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var path = context.Request.Path.Value;
        _logger.LogInformation($"original path is '{path}'");
        if (context.GetEndpoint() != null)
        {
            await _next(context);
            return;
        }

        if(context.Request.PathBase.HasValue)
        {
            await _next(context);
            return;
        }
        _logger.LogInformation(path);
        var first = !string.IsNullOrEmpty(path) && path != "/" ? path.Split('/', StringSplitOptions.RemoveEmptyEntries)[0] : "";
        _logger.LogInformation( _supportedCultureOptions.DefaultCultureRoute );
        
        if (!_supportedCultureOptions.SupportedCultures.ContainsKey(first))
        {
            context.Response.Redirect($"/{_supportedCultureOptions.DefaultCultureRoute}{context.Request.Path} ");
            return;
        }
        await _next(context);
    }
}