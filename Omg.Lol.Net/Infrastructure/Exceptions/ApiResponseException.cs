namespace Omg.Lol.Net.Infrastructure.Exceptions;

using System;
using Omg.Lol.Net.Models;

public class ApiResponseException : Exception
{
    public override string Message => this.ServerResponse.Response.Message;

    public int StatusCode => this.ServerResponse.Request.StatusCode;

    public bool Success => this.ServerResponse.Request.Success;

    public CommonResponse<ResponseMessage> ServerResponse { get; }

    public ApiResponseException(CommonResponse<ResponseMessage> error)
    {
        this.ServerResponse = error;
    }
}
