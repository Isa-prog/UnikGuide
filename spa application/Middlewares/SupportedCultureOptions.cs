namespace Destinationosh.Middlewares;

public class SupportedCultureOptions
{
    public string DefaultCultureRoute { get; set; } = "en";
    public IDictionary<string, string> SupportedCultures { get; set; } = new Dictionary<string, string>
    {
        {"en", "en"},
        {"ru", "ru"},
        {"kg", "ky"},
    };
}