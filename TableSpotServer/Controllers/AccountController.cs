using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TableSpot.Interfaces;
using TableSpot.Models;

namespace TableSpot.Controllers;

public class AccountController(IHttpResponseJsonService httpResponseJsonService) : ControllerBase
{
    [Authorize]
    [HttpGet("GetAccountData")]
    public ActionResult GetAccountData()
    {
        var id = 0;
        var value = User.FindFirst(ClaimTypes.Role)?.Value;
        if (Enum.TryParse(value, out AccountTypeModel accountTypeModel))
        {
            id = (int)accountTypeModel;
        }
        var account = new
        {
            Email = User.Identity?.Name,
            AccountId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
            AccountType = new
            {
                Id = id,
                Type = User.FindFirst(ClaimTypes.Role)?.Value
            }
        };
        return Ok(httpResponseJsonService.Ok(account));
    }

}