using Destinationosh.Services;
using MatBlazor;
using Radzen;

namespace Destinationosh.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddXssSecurity(this IServiceCollection services)
        {
            services.AddTransient<IXssSecurity, XssSecurity>();
        }

        public static void AddSpaAdmin(this IServiceCollection services)
        {
            services.AddServerSideBlazor();
            services.AddMatBlazor();
            services.AddMatToaster(config =>
            {
                config.Position = MatToastPosition.BottomRight;
                config.PreventDuplicates = true;
                config.NewestOnTop = true;
                config.ShowCloseButton = true;
                config.MaximumOpacity = 95;
                config.VisibleStateDuration = 3000;
            });
            services.AddScoped<DialogService>();
        }
    }
}