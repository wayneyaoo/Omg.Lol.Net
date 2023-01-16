namespace Omg.Lol.Net.Models;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public class DnsRecordPost
{
    [JsonProperty("type")]
    [JsonConverter(typeof(StringEnumConverter))]
    public DnsRecordType Type { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("data")]
    public string Data { get; set; } = string.Empty;

    [JsonProperty("ttl")]
    public int? Ttl { get; set; }
}
