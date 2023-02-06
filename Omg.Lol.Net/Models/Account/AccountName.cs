namespace Omg.Lol.Net.Models.Account;

using Newtonsoft.Json;

public class AccountName
{
    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;
}
