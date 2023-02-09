namespace Omg.Lol.Net.Models.Account;

using System;
using Newtonsoft.Json;

public class AddressExpiration
{
    [JsonProperty("unix_epoch_time")]
    public long UnixEpochTime { get; set; }

    [JsonProperty("iso_8601_time")]
    public DateTimeOffset Iso8601Time { get; set; } = DateTimeOffset.MinValue;

    [JsonProperty("rfc_2822_time")]
    public string Rfc2822Time { get; set; } = string.Empty;

    [JsonProperty("relative_time")]
    public string RelativeTime { get; set; } = string.Empty;

    [JsonProperty("expired")]
    public bool Expired { get; set; }

    [JsonProperty("will_expire")]
    public bool WillExpire { get; set; }
}
