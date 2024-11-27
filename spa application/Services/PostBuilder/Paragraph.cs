using Newtonsoft.Json.Linq;

namespace Destinationosh.Services;

public class Paragraph : IBlockConverter
{
    public string Name => nameof(Paragraph);

    public string Convert(string json)
    {
        var jObject = JObject.Parse(json);
        var text = jObject["data"]["text"];
        if (text == null)
        {
            throw new ArgumentException("block does not contain text");
        }
        return $"<div class='post-block'><div class='post-block-content'><p>{text.Value<string>()}</p></div></div>";
    }
}
