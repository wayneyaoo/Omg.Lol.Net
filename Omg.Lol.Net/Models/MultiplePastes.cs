namespace Omg.Lol.Net.Models;

using Newtonsoft.Json;

public class MultiplePastes
{
    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("pastebin")]
    public PasteBrief[] Pastebin { get; set; }
}
