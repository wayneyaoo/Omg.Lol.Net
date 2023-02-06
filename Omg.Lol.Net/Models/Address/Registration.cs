namespace Omg.Lol.Net.Models.Address;

using System;
using Newtonsoft.Json;

/// <summary>
/// This a part of <see cref="PublicAddressInformation"/> model.
/// </summary>
public class Registration
{
    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;

    [JsonProperty("unix_epoch_time")]
    public long UnixEpochTime { get; set; }

    [JsonProperty("iso_8601_time")]
    public DateTimeOffset Iso8601Time { get; set; }

    [JsonProperty("rfc_2822_time")]
    public string Rfc2822Time { get; set; } = string.Empty;

    [JsonProperty("relative_time")]
    public string RelativeTime { get; set; } = string.Empty;
}
