namespace Omg.Lol.Net.Models.Dns;

using Newtonsoft.Json;

// TODO: Come back when this https://github.com/neatnik/omg.lol/issues/543 is fixed.
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
