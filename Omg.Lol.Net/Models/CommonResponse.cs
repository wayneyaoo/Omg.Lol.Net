namespace Omg.Lol.Net.Models;

using Newtonsoft.Json;

public class CommonResponse<T>
{
    [JsonProperty("request")]
    public Request Request { get; set; } = new ();

    [JsonProperty("response")]
    public T Response { get; set; } = default!;
}

public class Request
{
    [JsonProperty("status_code")]
    public int StatusCode { get; set; }

    [JsonProperty("success")]
    public bool Success { get; set; }
}
