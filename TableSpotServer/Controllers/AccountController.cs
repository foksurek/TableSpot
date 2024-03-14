using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TableSpot.Interfaces;
using TableSpot.Models;

namespace TableSpot.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class AccountController(
    IHttpResponseJsonService httpResponseJsonService,
    IRestaurantRepositoryService restaurantRepositoryService,
    IAccountRepositoryService accountRepositoryService
    ) : ControllerBase
{
    [Authorize]
    [HttpGet("GetAccountData")]
    public async Task<ActionResult> GetAccountData()
    {
        var id = 0;
        var value = User.FindFirst(ClaimTypes.Role)?.Value;
        if (Enum.TryParse(value, out AccountTypeModel accountTypeModel))
        {
            id = (int)accountTypeModel;
        }
        var data = await accountRepositoryService.GetAccount(User.Identity?.Name!);
        var account = new
        {
            Name = data!.Name,
            Surname = data!.Surname,
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
    
    [Authorize(Roles = nameof(AccountTypeModel.RestaurantOwner))]
    [HttpGet("GetMyRestaurants")]
    public async Task<ActionResult> GetMyRestaurants(int limit = 20, int offset = 0)
    {
        List<string> details = [];
        if (offset < 0) details.Add("Offset must be greater than or equal to 0");
        if (limit < 1) details.Add("Limit must be greater than 0");
        if (limit > 100) details.Add("Limit must be less than or equal to 100");

        if (details.Count > 0) return BadRequest(httpResponseJsonService.BadRequest(details));
        var data = await restaurantRepositoryService.GetRestaurantsByOwner(
            int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!),
            (int)limit!,
            (int)offset!
        );

        // return Ok(data);

        var responseData = data.Select(r => new RestaurantResponseModel()
        {
            Id = r.Id,
            Name = r.Name,
            Address = r.Address,
            Description = r.Description,
            ImageUrl = r.ImageUrl,
            Email = r.Email,
            Website = r.Website,
            PhoneNumber = r.PhoneNumber,
            Category = new
            {
                Id = r.Category.Id,
                Name = r.Category.Name
            }
        }).ToList();
        
        return Ok(httpResponseJsonService.Ok(responseData));
    }

}