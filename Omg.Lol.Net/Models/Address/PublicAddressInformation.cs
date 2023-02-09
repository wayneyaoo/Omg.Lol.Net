namespace Omg.Lol.Net.Models.Address;

using System.Collections.Generic;
using Newtonsoft.Json;

public class PublicAddressInformation
{
    [JsonProperty("address")]
    public string Address { get; set; } = string.Empty;

    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;

    [JsonProperty("registration")]
    public Registration Registration { get; set; } = new ();

    [JsonProperty("expiration")]
    public AddressExpirationPublicView ExpirationPublicView { get; set; } = new ();

    [JsonProperty("verification")]
    public Verification Verification { get; set; } = new ();

    [JsonProperty("keys")]
    public Dictionary<string, string[]> Keys { get; set; } = new ();
}
