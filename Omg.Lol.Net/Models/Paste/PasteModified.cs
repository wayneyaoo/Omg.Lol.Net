namespace Omg.Lol.Net.Models.Paste;

using Newtonsoft.Json;

public class PasteModified
{
    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; } = string.Empty;
}
