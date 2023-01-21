﻿namespace Omg.Lol.Net.Models;

using Newtonsoft.Json;

public class PurlPost
{
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("url")]
    public string Url { get; set; } = string.Empty;
}
