namespace Omg.Lol.Net.Models.Directory;

using System;

public class AddressDirectory
{
    public string Message { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;

    public string[] Directory { get; set; } = Array.Empty<string>();
}
