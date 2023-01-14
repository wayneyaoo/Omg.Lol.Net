namespace Omg.Lol.Net.Models;

using Newtonsoft.Json;

public class SinglePaste
{
    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("paste")]
    public PasteDetail PasteDetail { get; set; }
}
