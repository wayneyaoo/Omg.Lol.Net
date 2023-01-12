namespace Omg.Lol.Net.Models;

using Newtonsoft.Json;

// For Non-200 response, only message field is captured.
public class ErrorMessage
{
    [JsonProperty("message")]
    public string Message { get; set; }
}
