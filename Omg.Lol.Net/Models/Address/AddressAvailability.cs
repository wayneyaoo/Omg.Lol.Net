namespace Omg.Lol.Net.Models.Address;

using System;
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

    /// <summary>
    /// Not empty if the address needs encoding. e.g., 😊 will get encoded (if available). Otherwise empty.
    /// </summary>
    [JsonProperty("punycode")]
    public string PunyCode { get; set; } = string.Empty;

    /// <summary>
    /// Not empty if the address needs encoding. Otherwise emoty.
    /// </summary>
    [JsonProperty("see-also")]
    public string[] SeeAlso { get; set; } = Array.Empty<string>();
}
