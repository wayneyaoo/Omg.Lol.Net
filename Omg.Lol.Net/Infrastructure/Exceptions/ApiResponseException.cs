namespace Omg.Lol.Net.Infrastructure.Exceptions;

using System;
using Omg.Lol.Net.Models;

public class ApiResponseException : Exception
{
    public CommonResponse<ErrorMessage> ServerResponse { get; }

    public ApiResponseException(CommonResponse<ErrorMessage> error)
    {
        this.ServerResponse = error;
    }
}
