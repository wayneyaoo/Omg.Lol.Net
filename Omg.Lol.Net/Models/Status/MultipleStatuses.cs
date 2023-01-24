namespace Omg.Lol.Net.Models.Status;

using System;
using Newtonsoft.Json;

public class MultipleStatuses
{
    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;

    [JsonProperty("statuses")]
    public Models.Status.Status[] Statuses { get; set; } = Array.Empty<Models.Status.Status>();
}
