namespace Omg.Lol.Net.Models;

using System;
using Newtonsoft.Json;

public class MultipleStatuses
{
    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;

    [JsonProperty("statuses")]
    public Status[] Statuses { get; set; } = Array.Empty<Status>();
}
