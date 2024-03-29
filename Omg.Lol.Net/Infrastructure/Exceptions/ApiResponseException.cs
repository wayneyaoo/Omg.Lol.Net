﻿namespace Omg.Lol.Net.Infrastructure.Exceptions;

using System;
using System.Net;
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

    public ApiResponseException(HttpStatusCode requestStatusCode)
    {
        this.ServerResponse = new CommonResponse<MessageItem>()
        {
            Request = new Request()
            {
                StatusCode = (int)requestStatusCode,
                Success = false,
            },

            Response = new MessageItem()
            {
                Message =
                    $"This message is generated by SDK because server returns an empty or invalid body. The raw request status code is: {(int)requestStatusCode}",
            },
        };
    }
}
