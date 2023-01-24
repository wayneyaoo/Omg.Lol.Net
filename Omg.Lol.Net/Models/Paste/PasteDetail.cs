namespace Omg.Lol.Net.Models.Paste;

using Newtonsoft.Json;

public class PasteDetail
{
    [JsonProperty("modified_on")]
    public long? ModifiedOn { get; set; }

    [JsonProperty("content")]
    public string Content { get; set; } = string.Empty;

    [JsonProperty("title")]
    public string Title { get; set; }
    // [JsonProperty("listed")]
    // public bool Listed { get; set; }
}
