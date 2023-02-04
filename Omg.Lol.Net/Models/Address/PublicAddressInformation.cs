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
    public Registration Registration { get; set; }

    [JsonProperty("expiration")]
    public AddressExpirationPublicView ExpirationPublicView { get; set; }

    [JsonProperty("verification")]
    public Verification Verification { get; set; }

    [JsonProperty("keys")]
    public Dictionary<string, string[]> Keys { get; set; } = new ();
}
