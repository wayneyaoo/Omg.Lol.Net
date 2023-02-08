namespace Omg.Lol.Net.Models.Account;

using Newtonsoft.Json;

public class AccountInformation
{
    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;

    [JsonProperty("email")]
    public string Email { get; set; } = string.Empty;

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("settings")]
    public Settings Settings { get; set; } = new ();

    [JsonProperty("created")]
    public AccountCreationTime Created { get; set; } = new ();
}
