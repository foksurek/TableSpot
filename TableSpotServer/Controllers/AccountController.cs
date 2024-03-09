using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TableSpot.Interfaces;

namespace TableSpot.Controllers;

public class AccountController(IHttpResponseJsonService httpResponseJsonService) : ControllerBase
{
    [Authorize]
    [HttpGet("GetAccountData")]
    public ActionResult GetAccountData()
    {
        var account = new
        {
            Email = User.Identity?.Name,
            AccountId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
            AccountType = new
            {
                Id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                Type = User.FindFirst(ClaimTypes.Role)?.Value
            }
        };
        return Ok(httpResponseJsonService.Ok(account));
    }

}