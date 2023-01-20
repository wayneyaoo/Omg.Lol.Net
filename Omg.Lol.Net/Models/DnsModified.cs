namespace Omg.Lol.Net.Models;

using Newtonsoft.Json;

public class DnsModified
{
    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;

    [JsonProperty("data_sent")]
    public DnsRecordPost DataSent { get; set; }

    [JsonProperty("response_received")]
    public DnsCreateResponse ResponseReceived { get; set; }

    public class DnsCreateResponse
    {
        [JsonProperty("data")]
        public DnsRecordDetail Data { get; set; }
    }
}
