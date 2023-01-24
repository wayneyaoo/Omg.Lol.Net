namespace Omg.Lol.Net.Models.Dns;

using System;
using Newtonsoft.Json;

public class MultipleDnsRecords
{
    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;

    [JsonProperty("dns")]
    public DnsRecordDetail[] Dns { get; set; } = Array.Empty<DnsRecordDetail>();
}
