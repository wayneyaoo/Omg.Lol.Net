﻿namespace Omg.Lol.Net.Models;

using System;
using Newtonsoft.Json;

public class MultiplePastes
{
    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;

    [JsonProperty("pastebin")]
    public PasteBrief[] Pastebin { get; set; } = Array.Empty<PasteBrief>();
}
