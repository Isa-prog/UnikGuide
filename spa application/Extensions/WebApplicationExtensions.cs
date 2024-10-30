using Destinationosh.Middlewares;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;

namespace Destinationosh.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void UseSpaAdmin(this WebApplication app)
        {
            var options = app.Configuration.GetSection("SpaAdmin").Get<SpaAdminOptions>();

            app.UseStaticFiles($"/{options.UrlPath}");

            app.UsePathBase($"/{options.UrlPath}");
            app.MapBlazorHub($"/{options.UrlPath}/_blazor", options =>
            {
                options.CloseOnAuthenticationExpiration = true;
            }).RequireAuthorization();
            app.MapFallbackToPage("/admin/{*path:nonfile}", "/Admin");
        }

        public static void UseCultureRedirect(this WebApplication app)
        {
            app.UseMiddleware<CultureRedirectMiddleware>();

            app.UseRequestLocalization(options =>
            {
                var cultureOption = app.Services.GetRequiredService<IOptions<SupportedCultureOptions>>();
                var cultures = cultureOption.Value.SupportedCultures.Values.ToArray();
                options.SetDefaultCulture(
                    cultureOption.Value.SupportedCultures[cultureOption.Value.DefaultCultureRoute]
                );
                options.AddSupportedCultures(
                    cultures
                );
                options.AddSupportedUICultures(
                    cultures
                );

                options.RequestCultureProviders.Clear();
                options.RequestCultureProviders.Add(
                    new RouteCultureProvider( 
                        app.Services.GetRequiredService<ILogger<RouteCultureProvider>>(),
                        cultureOption
                    )
                );
                options.RequestCultureProviders.Add(
                    new CookieRequestCultureProvider()
                );
            });
        }
    }
}