namespace Omg.Lol.Net.Models.Now;

using System;
using Newtonsoft.Json;

public class NowGarden
{
    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;

    [JsonProperty("garden")]
    public Garden[] Gardens { get; set; } = Array.Empty<Garden>();
}

public class Garden
{
    [JsonProperty("address")]
    public string Address { get; set; } = string.Empty;

    [JsonProperty("url")]
    public string Url { get; set; } = string.Empty;

    [JsonProperty("updated")]
    public GardenUpdateTime Updated { get; set; } = new ();
}
