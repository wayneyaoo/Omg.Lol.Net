namespace Omg.Lol.Net.Models.Account;

using Newtonsoft.Json;

public class AccountSession
{
    [JsonProperty("session_id")]
    public string SessionId { get; set; } = string.Empty;

    [JsonProperty("user_agent")]
    public string UserAgent { get; set; } = string.Empty;

    [JsonProperty("created_ip")]
    public string CreatedIp { get; set; } = string.Empty;

    [JsonProperty("created_on")]
    public long CreatedOn { get; set; } = long.MinValue;

    [JsonProperty("Expires_on")]
    public long ExpiresOn { get; set; } = long.MinValue;
}
