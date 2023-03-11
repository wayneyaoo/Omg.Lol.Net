namespace Omg.Lol.Net.Models.Now;

using Newtonsoft.Json;

public class Now
{
    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;

    [JsonProperty("now")]
    public NowContent NowContent { get; set; } = new ();
}
