namespace Omg.Lol.Net.Models;

using Newtonsoft.Json;

public class PastePost
{
    [JsonProperty("content")]
    public string Content { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("listed")]
    public bool Listed { get; set; }
}
