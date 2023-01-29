namespace Omg.Lol.Net.Models.Address;

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class AddressInformation
{
    [JsonProperty("address")]
    public string Address { get; set; } = string.Empty;

    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;

    [JsonProperty("registration")]
    public Registration Registration { get; set; }

    [JsonProperty("expiration")]
    public AddressExpiration Expiration { get; set; }

    [JsonProperty("verification")]
    public Verification Verification { get; set; }

    [JsonProperty("keys")]
    public Dictionary<string, string[]>? Keys { get; set; }

    /// <summary>
    /// The owner information is only available when request with bearer token.
    /// </summary>
    [JsonProperty("owner")]
    public string? Owner { get; set; }
}

/// <summary>
/// This a part of <see cref="AddressInformation"/> model.
/// </summary>
public class Registration
{
    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;

    [JsonProperty("unix_epoch_time")]
    public long? UnixEpochTime { get; set; }

    [JsonProperty("iso_8601_time")]
    public DateTimeOffset? Iso8601_Time { get; set; }

    [JsonProperty("rfc_2822_time")]
    public string? Rfc2822_Time { get; set; }

    [JsonProperty("relative_time")]
    public string? RelativeTime { get; set; }
}

/// <summary>
/// This a part of <see cref="AddressInformation"/> model.
/// </summary>
public class Verification
{
    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;

    [JsonProperty("verified")]
    public bool Verified { get; set; }
}
