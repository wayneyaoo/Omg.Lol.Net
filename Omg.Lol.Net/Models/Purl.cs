namespace Omg.Lol.Net.Models;

using Newtonsoft.Json;

public class Purl
{
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("url")]
    public string Url { get; set; } = string.Empty;

    // TODO: revisit this https://github.com/neatnik/omg.lol/issues/531 when resolved
    [JsonProperty("counter")]
    public string Counter { get; set; }

    // TODO: revisit this https://github.com/neatnik/omg.lol/issues/531 when resolved
    [JsonProperty("listed")]
    public string Listed { get; set; }
}
