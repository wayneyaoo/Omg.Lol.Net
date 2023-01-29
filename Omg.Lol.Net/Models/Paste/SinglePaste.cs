namespace Omg.Lol.Net.Models.Paste;

using Newtonsoft.Json;

public class SinglePaste
{
    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;

    [JsonProperty("paste")]
    public PasteDetail PasteDetail { get; set; }
}
