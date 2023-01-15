namespace Omg.Lol.Net.Models.Items;

using Newtonsoft.Json;

public class ContentItem
{
    [JsonProperty("content")]
    public string Content { get; set; } = string.Empty;
}
