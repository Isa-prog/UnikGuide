using Newtonsoft.Json.Linq;

namespace Destinationosh.Services;

public class Header : IBlockConverter
{
    public string Name => nameof(Header);

    public string Convert(string json)
    {
        var jObject = JObject.Parse(json);
        var text = jObject["data"]["text"];
        var level = jObject["data"]["level"].Value<int>();
        if (text == null)
        {
            throw new ArgumentException("block does not contain text");
        }
        if (level < 1 || level > 6)
        {
            throw new ArgumentException("level must be between 1 and 6");
        }
        return $"<div class='post-block'><div class='post-block-content'><h{level}>{text.Value<string>()}</h{level}></div></div>";
    }
}
