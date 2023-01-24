namespace Omg.Lol.Net.Models.Dns;

using System;
using Newtonsoft.Json;

public class DnsRecordDetail
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("type")]
    public string Type { get; set; } = string.Empty;

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("data")]
    public string Data { get; set; } = string.Empty;

    [JsonProperty("priority")]
    public int? Priority { get; set; }

    [JsonProperty("ttl")]
    public int Ttl { get; set; }

    [JsonProperty("created_at")]
    public DateTimeOffset CreatedAt { get; set; }

    [JsonProperty("updated_at")]
    public DateTimeOffset UpdatedAt { get; set; }
}
