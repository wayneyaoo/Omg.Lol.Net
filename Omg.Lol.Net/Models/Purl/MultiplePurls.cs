namespace Omg.Lol.Net.Models.Purl;

using System;
using Newtonsoft.Json;

public class MultiplePurls
{
    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;

    [JsonProperty("purls")]
    public Purl[] Purls { get; set; } = Array.Empty<Purl>();
}
