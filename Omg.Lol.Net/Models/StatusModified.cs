namespace Omg.Lol.Net.Models;

using Newtonsoft.Json;

public class StatusModified
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("url")]
    public string Url { get; set; } = string.Empty;

    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;
}
