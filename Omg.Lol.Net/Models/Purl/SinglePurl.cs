namespace Omg.Lol.Net.Models.Purl;

using Newtonsoft.Json;

public class SinglePurl
{
    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;

    [JsonProperty("purl")]
    public Purl Purl { get; set; }
}
