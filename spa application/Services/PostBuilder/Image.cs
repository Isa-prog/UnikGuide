using System.Text;
using Newtonsoft.Json.Linq;

namespace Destinationosh.Services;

public class Image : IBlockConverter
{
    public string Name => nameof(Image);

    public string Convert(string json)
    {
        var jObject = JObject.Parse(json);
        var src = jObject["data"]["file"]["url"].Value<string>();
        var caption = jObject["data"]["caption"].Value<string>();
        var stretched = jObject["data"]["stretched"].Value<bool>();
        var withBackground = jObject["data"]["withBackground"].Value<bool>();
        var withBorder = jObject["data"]["withBorder"].Value<bool>();

        var block = new StringBuilder();
        var blockContent = new StringBuilder();
        if (string.IsNullOrWhiteSpace(src))
        {
            throw new ArgumentException("block does not contain src");
        }
        block.Append("<div class='post-block ");
        if (stretched)
        {
            block.Append("stretched ");
        }

        if (withBackground)
        {
            block.Append("with-background ");
        }
        if(withBorder)
        {
            block.Append("with-border");
        }
        block.Append("'>");

        blockContent.Append("<div class='post-block-content'>");
        blockContent.Append($"<img src='{src}' alt='{caption}'/>");
        if(!string.IsNullOrWhiteSpace(caption))
        {
            blockContent.Append($"<p class='text-center'>{caption}</p>");
        }
        blockContent.Append("</div>");

        block.Append(blockContent);

        block.Append("</div>");

        return block.ToString();
    }
}
