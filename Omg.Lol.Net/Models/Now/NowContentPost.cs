namespace Omg.Lol.Net.Models.Now;

using Newtonsoft.Json;

public class NowContentPost
{
    [JsonProperty("content")]
    public string Content { get; set; } = string.Empty;

    [JsonProperty("listed")]
    public string Listed { get; set; } = string.Empty;
}
