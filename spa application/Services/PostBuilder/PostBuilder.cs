using System.Text;
using Newtonsoft.Json.Linq;

namespace Destinationosh.Services;

public class PostBuilder
{
    public IDictionary<string, IBlockConverter> converters = new Dictionary<string, IBlockConverter>(StringComparer.OrdinalIgnoreCase);
    public bool IgnoreUnRegisteredBlocks { get; set; } = false;
    public bool IgnoreBadBlocks { get; set; } = false;
    public PostBuilder AddConverter(IEnumerable<IBlockConverter> converters)
    {
        foreach(var converter in converters)
        {
            this.converters.Add(converter.Name, converter);
        }
        return this;
    }

    public string Build(string json)
    {
        var jObject = JObject.Parse(json);
        var blocks = jObject["blocks"] as JArray;
        if (blocks == null)
        {
            throw new ArgumentException("json does not contain blocks");
        }
        var result = new StringBuilder();
        foreach (var block in blocks)
        {
            var type = block["type"];
            if (type == null)
            {
                throw new ArgumentException("block does not contain type");
            }
            if (!converters.ContainsKey(type.Value<string>()) && !IgnoreUnRegisteredBlocks)
            {
                throw new ArgumentException($"block type {type} is not registered");
            }
            var converter = converters[type.ToString()];
            try
            {
                result.Append(converter.Convert(block.ToString()));
            }
            catch (Exception exception)
            {
                if (!IgnoreBadBlocks)
                {
                    throw exception;
                }
            }
        }
        return result.ToString();
    }
}