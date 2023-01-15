namespace Omg.Lol.Net.Models;

using Newtonsoft.Json;

public class StatusPost
{
    [JsonProperty("emoji")]
    public string Emoji { get; set; } = string.Empty;

    [JsonProperty("content")]
    public string Content { get; set; } = string.Empty;
}
