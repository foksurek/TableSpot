using System.Collections;
using System.Reflection.Metadata;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using TableSpot.Interfaces;

namespace TableSpot.Services;

public class HttpResponseJsonService : IHttpResponseJsonService
{
    public object Ok(object data)
    {
        return new { code = 200, data };
    }

    public object BadRequest(List<string> details)
    {
        details ??= [];
        return new { Code = 400, Message = "Bad request", Details = details };
    }

    public object NotFound(string message = "Not found")
    {
        return new { Code = 404, Message = message };
    }

    public object Unauthorized(List<string> details = null!)
    {
        details ??= [];
        return new { Code = 403, Message = "Unauthorized", Details = details };
    }
}