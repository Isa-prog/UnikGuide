using HtmlAgilityPack;

namespace Destinationosh.Services;
public class XssSecurity : IXssSecurity
{
    private static readonly string[] _allowedTags = new string[]
    {
            "p",
            "h1",
            "h2",
            "h3",
            "img",
            "ul",
            "ol",
            "li",
            "strong",
            "em",
            "u",
            "br",
    };

    private static readonly string[] _allowedAttributes = new string[]
    {
        "class",
        "style"
    };

    private readonly ILogger logger;
    private readonly HttpContext http;

    public XssSecurity(ILogger<IXssSecurity> logger, IHttpContextAccessor accessor)
    {
        this.logger = logger;
        this.http = accessor.HttpContext ?? throw new ArgumentNullException(nameof(accessor));
    }

    public bool CheckPost(string rawHtml)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(rawHtml);

        foreach (var node in doc.DocumentNode.SelectNodes("//*"))
        {
            if (!_allowedTags.Contains(node.Name))
            {
                return false;
            }
            if (node.HasAttributes && !CheckAttributes(node))
            {
                return false;
            }
        }
        return true;
    }

    public bool CheckUrl(string url)
    {
        try
        {
            var uri = new Uri(url);
            if (uri.Host == http.Request.Host.Host)
            {
                return true;
            }
        }
        catch (Exception exc)
        {
            logger.LogError(exc.Message);
        }
        return false;
    }

    private bool CheckAttributes(HtmlNode node)
    {
        foreach (var attribute in node.Attributes)
        {
            if (!_allowedAttributes.Contains(attribute.Name))
            {
                return false;
            }
            else if (node.Name != "img" || attribute.Name != "src" || !CheckUrl(attribute.Value))
            {
                return false;
            }
            else if( CheckJs(attribute.Value))
            {
                return false;
            }
        }
        return true;
    }

    private bool CheckJs(string str)
    {
        return str.Contains("javascript:") || str.Contains("expression");
    }
}
