namespace Omg.Lol.Net.Models.Web;

using Newtonsoft.Json;

public class WebPageUpdate
{
    [JsonProperty("publish")]
    public bool Publish { get; set; }

    [JsonProperty("content")]
    public string Content { get; set; } = string.Empty;
}
