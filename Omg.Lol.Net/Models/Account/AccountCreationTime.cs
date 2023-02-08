namespace Omg.Lol.Net.Models.Account;

using System;
using Newtonsoft.Json;

public class AccountCreationTime
{
    [JsonProperty("unix_epoch_time")]
    public long UnixEpochTime { get; set; } = long.MinValue;

    [JsonProperty("iso_8601_time")]
    public DateTimeOffset Iso8601Time { get; set; } = DateTimeOffset.MinValue;

    [JsonProperty("rfc_2822_time")]
    public string Rfc2822Time { get; set; } = string.Empty;

    [JsonProperty("relative_time")]
    public string RelativeTime { get; set; } = string.Empty;
}
