namespace Omg.Lol.Net.Models.Address;

using Newtonsoft.Json;

public class AddressExpirationPublicView
{
    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;

    [JsonProperty("expired")]
    public bool Expired { get; set; }
}
