namespace Omg.Lol.Net.Models.Paste;

using System;
using Newtonsoft.Json;

/// <summary>
/// Compared with <see cref="PasteDetail"/>, for the time being the only difference is "listed" field.
/// </summary>
public class PasteBrief
{
    [JsonProperty("content")]
    public string Content { get; set; } = string.Empty;

    [JsonProperty("modified_on")]
    public long? ModifiedOn { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; } = string.Empty;
}
