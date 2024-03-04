using System.Collections;
using Microsoft.AspNetCore.Mvc;

namespace TableSpot.Interfaces;

public interface IHttpResponseJsonService
{
    public object Ok(object data);
    public object BadRequest(List<string> details = null!);
    public object NotFound(string message = "Not found");
}