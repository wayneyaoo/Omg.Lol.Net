namespace Omg.Lol.Net.Models;

using Newtonsoft.Json;

public class StatusBio
{
    [JsonProperty("css")]
    public string Css { get; set; } = string.Empty;

    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;

    [JsonProperty("bio")]
    public string Bio { get; set; } = string.Empty;
}
