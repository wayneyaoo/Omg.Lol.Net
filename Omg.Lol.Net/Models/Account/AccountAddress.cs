namespace Omg.Lol.Net.Models.Account;

using Newtonsoft.Json;
using Omg.Lol.Net.Models.Address;

public class AccountAddress
{
    [JsonProperty("address")]
    public string Address { get; set; } = string.Empty;

    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;

    [JsonProperty("registration")]
    public Registration Registration { get; set; } = new ();

    [JsonProperty("expiration")]
    public AddressExpiration Expiration { get; set; } = new ();
}
