using Destinationosh.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;

namespace Destinationosh.Middlewares;

public class RouteCultureProvider : IRequestCultureProvider
{
    private readonly ILogger<RouteCultureProvider> _logger;
    private readonly SupportedCultureOptions _supportedCultureOptions;
    public RouteCultureProvider(ILogger<RouteCultureProvider> logger, IOptions<SupportedCultureOptions> option)
    {
        _logger = logger;
        _supportedCultureOptions = option.Value;
    }

    public Task<ProviderCultureResult?> DetermineProviderCultureResult(HttpContext httpContext = null!)
    {
        if(httpContext.Request.PathBase.HasValue)
        {
            return Task.FromResult<ProviderCultureResult?>(null);
        }
        
        var path = httpContext.Request.Path.Value;
        var first = !string.IsNullOrEmpty(path) && path != "/"  ? path.Split('/', StringSplitOptions.RemoveEmptyEntries)[0] : "";
        
        if (_supportedCultureOptions.SupportedCultures.ContainsKey(first))
        {
            _logger.LogInformation(first);
            return Task.FromResult<ProviderCultureResult?>(new ProviderCultureResult(_supportedCultureOptions.SupportedCultures[first], _supportedCultureOptions.SupportedCultures[first]));
        }
        return Task.FromResult<ProviderCultureResult?>(null);
    }
}