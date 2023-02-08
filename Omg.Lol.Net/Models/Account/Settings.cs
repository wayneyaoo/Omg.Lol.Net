namespace Omg.Lol.Net.Models.Account;

using Newtonsoft.Json;

public class Settings
{
    [JsonProperty("communication")]
    public string Communication { get; set; } = string.Empty;

    [JsonProperty("owner")]
    public string Owner { get; set; } = string.Empty;

    [JsonProperty("date_format")]
    public string? DateFormat { get; set; } = string.Empty;

    [JsonProperty("web_editor")]
    public string? WebEditor { get; set; } = string.Empty;
}
