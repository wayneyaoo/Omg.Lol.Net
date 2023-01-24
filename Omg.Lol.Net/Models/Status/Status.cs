namespace Omg.Lol.Net.Models.Status;

using Newtonsoft.Json;

public class Status
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("address")]
    public string Address { get; set; } = string.Empty;

    [JsonProperty("created")]
    public long Created { get; set; }

    [JsonProperty("relative_time")]
    public string RelativeTime { get; set; } = string.Empty;

    [JsonProperty("emoji")]
    public string Emoji { get; set; } = string.Empty;

    [JsonProperty("content")]
    public string Content { get; set; } = string.Empty;
}
