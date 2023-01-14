namespace Omg.Lol.Net.Models;

using Newtonsoft.Json;

public class PasteDetail
{
    [JsonProperty("content")]
    public string Content { get; set; }

    [JsonProperty("modified_on")]
    public long? ModifiedOn { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }
    // [JsonProperty("listed")]
    // public bool Listed { get; set; }
}
