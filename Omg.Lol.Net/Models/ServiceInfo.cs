namespace Omg.Lol.Net.Models;

using Newtonsoft.Json;

public class ServiceInfo
{
    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("members")]
    public int Members { get; set; }

    [JsonProperty("addresses")]
    public int Addresses { get; set; }

    [JsonProperty("profiles")]
    public int Profiles { get; set; }
}
