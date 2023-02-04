namespace Omg.Lol.Net.Models.Address;

using Newtonsoft.Json;

/// <summary>
/// This a part of <see cref="PublicAddressInformation"/> model.
/// </summary>
public class Verification
{
    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;

    [JsonProperty("verified")]
    public bool Verified { get; set; }
}
