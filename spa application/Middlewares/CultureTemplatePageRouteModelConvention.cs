using System;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Logging;

namespace Destinationosh.Middlewares; 

public class CultureTemplatePageRouteModelConvention : IPageRouteModelConvention
{
    public void Apply(PageRouteModel model)
    {
        // For each page Razor has detected
        foreach (var selector in model.Selectors)
        {
            // Grab the template string
            var template = selector.AttributeRouteModel.Template;

            // Skip the MicrosoftIdentity pages
            if (template.StartsWith("MicrosoftIdentity")) continue;

            // Skip the Admin page
            if (template.StartsWith("Admin", StringComparison.OrdinalIgnoreCase)) continue;
            
            // Prepend the /{culture?}/ route value to allow for route-based localization
            selector.AttributeRouteModel.Template = AttributeRouteModel.CombineTemplates("{culture}", template);
        }
    }
}