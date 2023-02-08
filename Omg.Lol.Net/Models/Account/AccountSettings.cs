namespace Omg.Lol.Net.Models.Account;

using Newtonsoft.Json;

public class AccountSettings
{
    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;

    [JsonProperty("settings")]
    public Settings Settings { get; set; } = new ();
}
