namespace Omg.Lol.Net.Models.Status;

using Newtonsoft.Json;

public class SingleStatus
{
    [JsonProperty("status")]
    public Status Status { get; set; } = new ();

    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;
}
