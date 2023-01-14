namespace Omg.Lol.Net.Models;

using Newtonsoft.Json;

// For Non-200 response and some API calls, only message field is populated and captured.
public class ResponseMessage
{
    [JsonProperty("message")]
    public string Message { get; set; }
}
