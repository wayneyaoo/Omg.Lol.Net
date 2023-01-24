namespace Omg.Lol.Net.Models.Paste;

using Newtonsoft.Json;

public class PastePost
{
    [JsonProperty("content")]
    public string Content { get; set; } = string.Empty;

    [JsonProperty("title")]
    public string Title { get; set; } = string.Empty;

    [JsonProperty("listed")]
    public bool Listed { get; set; }
}
