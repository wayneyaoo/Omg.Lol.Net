namespace Omg.Lol.Net.Models.Dns;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public class DnsRecordUpdate
{
    /// <summary>
    /// Id is mandatory when updating an existing record. 400 will be returned if not found.
    /// TODO: review this perodically to see if this gets fixed.
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("type")]
    [JsonConverter(typeof(StringEnumConverter))]
    public DnsRecordType Type { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("data")]
    public string Data { get; set; } = string.Empty;

    [JsonProperty("ttl")]
    public int Ttl { get; set; }
}
