using System.Text;
using Newtonsoft.Json.Linq;

namespace Destinationosh.Services;

public class Carousel : IBlockConverter
{
    public string Name => nameof(Carousel);

    public string Convert(string json)
    {
        var jObject = JObject.Parse(json);
        var jArray = jObject["data"]["items"] as JArray;
        if(jArray == null)
        {
            throw new ArgumentException("block does not contain items");
        }
        
        Console.WriteLine("work");
        var stretched = jObject["data"]["stretched"].Value<bool?>();
        var id = jObject["id"].Value<string>() ?? "id"+Guid.NewGuid().ToString();

        var items = new CarouselItem[jArray.Count];
        
        for(int i = 0; i < jArray.Count; i++)
        {
            var item = jArray.ElementAt(i);
            var src = item["url"].Value<string>() ?? throw new ArgumentNullException("item does not contain src");
            var header = item["caption"]["header"].Value<string>() ?? throw new ArgumentNullException("item does not contain caption"); 
            var text = item["caption"]["text"].Value<string>() ?? throw new ArgumentNullException("item does not contain text");
            
            var buttonText = item["caption"]["button"]["text"].Value<string>();
            var buttonUrl = item["caption"]["button"]["url"].Value<string>();
            Console.WriteLine("url work");

            if(string.IsNullOrWhiteSpace(buttonText))
            {
                buttonText = null;
                buttonUrl = null;
            }
            items[i] = new CarouselItem(src, header, text,buttonText, buttonUrl);
        }
        Console.WriteLine("work2");

        var result = new StringBuilder();
        var block = new StringBuilder();
        var blockContent = new StringBuilder();

        block.Append("<div class='post-block ");
        if (stretched.HasValue && stretched.Value)
        {
            block.Append("stretched ");
        }
        block.Append("'>");

        blockContent.Append("<div class='post-block-content'>");
        blockContent.Append($"<div id={id} class='carousel slide' data-bs-ride='carousel' >");

        // Indicators
        blockContent.Append("<div class='carousel-indicators'>");
        for(int i = 0; i < items.Length; i++)
        {
            blockContent.Append($"<button data-bs-target='#{id}' data-bs-slide-to='{i}' class='{(i == 0 ? "active" : "")}'></button>");
        }
        blockContent.Append("</div>");
        // Indicators end

        // Carousel Inner
        blockContent.Append("<div class='carousel-inner'>");
        for(int i = 0; i < items.Length; i++)
        {
            //Item
            blockContent.Append($"<div class='carousel-item {(i == 0 ? "active" : "")}'>");

            blockContent.Append($"<img class='d-block' src='{items[i].src}' alt='{items[i].header}'/>");

            //Caption
            blockContent.Append("<div class='carousel-caption'>");
            if(!string.IsNullOrEmpty(items[i].header))blockContent.Append($"<h3>{items[i].header}</h3>");
            if(!string.IsNullOrEmpty(items[i].text))blockContent.Append($"<p>{items[i].text}</p>");
            if(items[i].buttonText != null)
            {
                blockContent.Append($"<a href='{items[i].buttonUrl}' class='btn btn-dark'>{items[i].buttonText}</a>");
            }
            blockContent.Append("</div>");
            //Caption end

            blockContent.Append("</div>");
            //Item end
        }
        blockContent.Append("</div>");
        //Carousel inner end

        blockContent.Append("<button class='carousel-control-prev' data-bs-slide='prev' data-bs-target='#"+id+"'><span class='carousel-control-prev-icon'></span></button>");
        blockContent.Append("<button class='carousel-control-next' data-bs-slide='next' data-bs-target='#"+id+"'><span class='carousel-control-next-icon'></span></button>");

        blockContent.Append("</div>");
        // Carousel end

        block.Append(blockContent);
        block.Append("</div>");

        return block.ToString();
    }

    struct CarouselItem
    {
        public string src;
        public string header;
        public string text;
        public string? buttonText;
        public string? buttonUrl;

        public CarouselItem(string src, string header, string text, string? buttonText, string? buttonUrl)
        {
            this.src = src;
            this.header = header;
            this.text = text;
            this.buttonText = buttonText;
            this.buttonUrl = buttonUrl;
        }
    }

}
