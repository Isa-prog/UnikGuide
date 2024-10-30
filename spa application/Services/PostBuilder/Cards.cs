using System.Text;
using Newtonsoft.Json.Linq;

namespace Destinationosh.Services;

public class Cards: IBlockConverter
{
    public string Name => nameof(Cards);

    public string Convert(string json)
    {
        var jObject = JObject.Parse(json);
        var cards = jObject["data"]["cards"] as JArray; 

        var block = new StringBuilder();
        var blockContent = new StringBuilder();
        block.Append("<div class='post-block'>");
        blockContent.Append("<div class='post-block-content'>");
        blockContent.Append("<div class='row'>");

        foreach(var card in cards)
        {
            var type = card["type"].Value<string>();
            if(type=="text")
            {
                AppendTextCard(blockContent, card);
            }
            if(type=="full")
            {
                AppendFullCard(blockContent, card);
            }
        }
        blockContent.Append("</div>");
        blockContent.Append("</div>");
        
        block.Append(blockContent);
        block.Append("</div>");

        return block.ToString();
    }

    void AppendTextCard(StringBuilder builder, JToken card)
    {
        var header = card["header"].Value<string>();
        var text = card["text"].Value<string>();

        builder.Append("<div class='info-card'>");
        builder.Append($"<h3>{header}</h3>");
        builder.Append($"<p>{text}</p>");
        builder.Append("</div>");
    }

    void AppendFullCard(StringBuilder builder, JToken card)
    {
        var header = card["header"].Value<string>();
        var text = card["text"].Value<string>();
        var src = card["imageUrl"].Value<string>();
        var linkText = card["linkText"].Value<string>();
        var linkUrl = card["linkUrl"].Value<string>();

        builder.Append("<div class='info-card'>");
        builder.Append($"<h3>{header}</h3>");
        builder.Append($"<img src='{src}' alt='{header}'/>");
        builder.Append($"<p>{text}</p>");
        builder.Append($"<a href='{linkUrl}' class='btn btn-dark'>{linkText}</a>");
        builder.Append("</div>");
    }
}