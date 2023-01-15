namespace Omg.Lol.Net.Models;

using Newtonsoft.Json;

public class AddressAvailability
{
    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;

    [JsonProperty("address")]
    public string Address { get; set; } = string.Empty;

    [JsonProperty("available")]
    public bool Available { get; set; }

    [JsonProperty("availability")]
    public string Availability { get; set; } = string.Empty;
}
