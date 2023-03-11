namespace Omg.Lol.Net.Models.Now;

using Newtonsoft.Json;

public class NowContent
{
    [JsonProperty("content")]
    public string Content { get; set; } = string.Empty;

    [JsonProperty("updated")]
    public long Updated { get; set; }

    [JsonProperty("listed")]
    public string Listed { get; set; } = string.Empty;
}