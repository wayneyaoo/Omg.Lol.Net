namespace Omg.Lol.Net.Models;

using System;
using Newtonsoft.Json;

public class AddressExpiration
{
    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("unix_epoch_time")]
    public long? UnixEpochTime { get; set; }

    [JsonProperty("iso_8601_time")]
    public DateTimeOffset? Iso8601_Time { get; set; }

    [JsonProperty("rfc_2822_time")]
    public string? Rfc2822_Time { get; set; }

    [JsonProperty("relative_time")]
    public string? RelativeTime { get; set; }

    [JsonProperty("expired")]
    public bool Expired { get; set; }

    [JsonProperty("will_expire")]
    public bool WillExpire { get; set; }
}
