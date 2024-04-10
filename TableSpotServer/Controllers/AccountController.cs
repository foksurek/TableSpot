using System.Security.Claims;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mysqlx;
using TableSpot.Interfaces;
using TableSpot.Models;
using TableSpot.Services;
using TableSpot.Utils;

namespace TableSpot.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class AccountController(
    IHttpResponseJsonService httpResponseJsonService,
    IRestaurantRepositoryService restaurantRepositoryService,
    IAccountRepositoryService accountRepositoryService,
    AuthService authService
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
        if (data == null)
        {
            await authService.SignOutAsync(HttpContext);
            return NotFound(httpResponseJsonService.NotFound("Account not found"));
        }
        
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
        switch (limit)
        {
            case < 1:
                details.Add("Limit must be greater than 0");
                break;
            case > 100:
                details.Add("Limit must be less than or equal to 100");
                break;
        }

        if (details.Count > 0) return BadRequest(httpResponseJsonService.BadRequest(details));
        var data = await restaurantRepositoryService.GetRestaurantsByOwner(
            int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!),
            limit!,
            offset!
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
    
    

    
    [Authorize]
    [HttpPost("ParseRecentlySeenRestaurants")]
    public async Task<ActionResult> ParseRecentlySeenRestaurants([FromBody] IdListModel model)
    {
        var restaurantIds = model.Ids;
        if (restaurantIds == null) return NotFound(httpResponseJsonService.NotFound("Nothing has been found"));
        if (restaurantIds.Count == 0) return NotFound(httpResponseJsonService.NotFound("Nothing has been found"));
        List<RestaurantResponseModel> data = [];
        foreach (var id in restaurantIds.Where(id => !string.IsNullOrEmpty(id)))
        {
            if (!Regexes.CheckIfNumber().IsMatch(id)) continue;
            var restaurant = await restaurantRepositoryService.GetRestaurantById(int.Parse(id));
            if (restaurant == null) continue;
            data.Add(new RestaurantResponseModel()
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Address = restaurant.Address,
                Description = restaurant.Description,
                ImageUrl = restaurant.ImageUrl,
                Email = restaurant.Email,
                Website = restaurant.Website,
                PhoneNumber = restaurant.PhoneNumber,
                Category = restaurant.Category
            });
        }

        if (data.Count == 0)
            return NotFound(httpResponseJsonService.NotFound("Nothing has been found"));
        return Ok(httpResponseJsonService.Ok(data));
    }
    
}