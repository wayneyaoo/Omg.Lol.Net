﻿namespace Omg.Lol.Net.Models;

using Newtonsoft.Json;

public class SingleStatus
{
    [JsonProperty("status")]
    public Status Status { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;
}
