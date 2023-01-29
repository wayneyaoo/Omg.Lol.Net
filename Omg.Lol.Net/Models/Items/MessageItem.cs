namespace Omg.Lol.Net.Models.Items;

using Newtonsoft.Json;

public class MessageItem
{
    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;
}
