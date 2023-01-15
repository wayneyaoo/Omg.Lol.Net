namespace Omg.Lol.Net.Infrastructure.Exceptions;

using System;
using Omg.Lol.Net.Models;
using Omg.Lol.Net.Models.Items;

public class ApiResponseException : Exception
{
    public override string Message => this.ServerResponse.Response.Message;

    public int StatusCode => this.ServerResponse.Request.StatusCode;

    public bool Success => this.ServerResponse.Request.Success;

    public CommonResponse<MessageItem> ServerResponse { get; }

    public ApiResponseException(CommonResponse<MessageItem> error)
    {
        this.ServerResponse = error;
    }
}
