namespace Omg.Lol.Net.Models;

using Newtonsoft.Json;

public class Purl
{
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("url")]
    public string Url { get; set; } = string.Empty;

    [JsonProperty("counter")]
    public int? Counter { get; set; }
    // [JsonProperty("listed")]
    // public bool Listed { get; set; }
}
