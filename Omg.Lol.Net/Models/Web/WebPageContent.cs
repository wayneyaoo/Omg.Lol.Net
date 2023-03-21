namespace Omg.Lol.Net.Models.Web;

using Newtonsoft.Json;

public class WebPageContent
{
    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;

    [JsonProperty("content")]
    public string Content { get; set; } = string.Empty;

    [JsonProperty("type")]
    public string Type { get; set; } = string.Empty;

    [JsonProperty("theme")]
    public string Theme { get; set; } = string.Empty;

    [JsonProperty("css")]
    public string Css { get; set; } = string.Empty;

    [JsonProperty("head")]
    public string Head { get; set; } = string.Empty;

    [JsonProperty("verified")]
    public string Verified { get; set; } = string.Empty;

    [JsonProperty("pfp")]
    public string ProfilePicture { get; set; } = string.Empty;

    [JsonProperty("metadata")]
    public string Metadata { get; set; } = string.Empty;

    [JsonProperty("branding")]
    public string Branding { get; set; } = string.Empty;
}
