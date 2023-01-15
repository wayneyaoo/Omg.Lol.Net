namespace Omg.Lol.Net.Models;

using Newtonsoft.Json;
using Omg.Lol.Net.Models.Items;

public class PasteDetail : ContentItem
{
    [JsonProperty("modified_on")]
    public long? ModifiedOn { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }
    // [JsonProperty("listed")]
    // public bool Listed { get; set; }
}
