using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TableSpot.Dto;
using TableSpot.Interfaces;
using TableSpot.Models;
using TableSpot.Services;


namespace TableSpot.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class RestaurantController(
    IRestaurantRepositoryService restaurantRepositoryService,
    IHttpResponseJsonService httpResponseJson,
    AppDbContext dbContext
) : ControllerBase
{
    [HttpGet("GetAll")]
    public async Task<ActionResult> GetAllRestaurants(int limit = 10, int offset = 0)
    {
        List<string> details = [];
        if (limit < 0) details.Add("Limit must be greater than 0");
        if (offset < 0) details.Add("Offset must be greater than 0");
        if (limit > 100) details.Add("Limit must be less than 100");
        if (details.Count > 0) return BadRequest(httpResponseJson.BadRequest(details));
        
        var data = await restaurantRepositoryService.GetAllRestaurants(limit, offset);

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
        
        
        return Ok(httpResponseJson.Ok(responseData));
    }
    
    [HttpGet("GetById")]
    public async Task<ActionResult> GetRestaurantById([Required] int? id)
    {
        if (!id.HasValue) return BadRequest(httpResponseJson.BadRequest(["Restaurant id must be correct"]));
        var data = await restaurantRepositoryService.GetRestaurantById((int)id!);
        if (data == null) return NotFound(httpResponseJson.NotFound("Restaurant not found"));
        
        return Ok(httpResponseJson.Ok(data));
    }
    
    [HttpGet("Search")]
    public async Task<ActionResult> Search(string query = "", int limit = 10, int offset = 0)
    {
        List<string> details = [];
        if (limit < 0 || offset < 0) details.Add("Limit and offset must be greater than 0");
        if (limit > 100) details.Add("Limit must be less than 100");
        if (details.Count > 0) return BadRequest(httpResponseJson.BadRequest(details));
        var data = await restaurantRepositoryService.GetRestaurantsByNameOrDescription(query, limit, offset);
        
        return Ok(httpResponseJson.Ok(data));
    }
    
    [HttpGet("GetByCategory")]
    public async Task<ActionResult> GetRestaurantByCategory([Required] int? categoryId, int limit = 10, int offset = 0)
    {
        List<string> details = [];
        if (categoryId < 0 || !categoryId.HasValue) details.Add("Category id must be correct");
        if (limit < 0 || offset < 0) details.Add("Limit and offset must be greater than 0");
        if (limit > 100) details.Add("Limit must be less than 100");
        if (details.Count > 0) return BadRequest(httpResponseJson.BadRequest(details));
        
        var data = await restaurantRepositoryService.GetRestaurantsByCategory((int)categoryId!, limit, offset);
        
        return Ok(httpResponseJson.Ok(data));
    }
    
    
    //
    // Authentication required endpoints
    //
    
    [Authorize(Roles = nameof(AccountTypeModel.RestaurantOwner) + "," +
                       nameof(AccountTypeModel.Admin))]
    [HttpPost("Create")]
    public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantModel model)
    {
        var restaurant = new RestaurantDto()
        {
            Name = model.Name,
            Address = model.Address,
            Description = model.Description,
            ImageUrl = model.ImageUrl,
            CategoryId = model.CategoryId,
            OwnerAccountId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!),
            Email = model.Email,
            Website = model.Website,
            PhoneNumber = model.PhoneNumber
        };
        
        if (restaurantRepositoryService.RestaurantExists(model.Name))
            return BadRequest(httpResponseJson.BadRequest(["Restaurant with this name already exists"]));
        if (!dbContext.Categories.Any(c => c.Id == model.CategoryId))
            return BadRequest(httpResponseJson.BadRequest(["Category with this id does not exist"]));
        await restaurantRepositoryService.CreateRestaurant(restaurant);
        return Ok(httpResponseJson.Ok("Restaurant created"));
    }
    
    // [Authorize(Roles = nameof(AccountTypeModel.RestaurantOwner))]
    // [HttpPost("ChangeName")]
    // public async Task<IActionResult> ChangeRestaurantName([Required] int? id, [Required] string name)
    // {
    //     if (!id.HasValue) return BadRequest(httpResponseJson.BadRequest(["Restaurant id must be correct"]));
    //     if (string.IsNullOrEmpty(name)) return BadRequest(httpResponseJson.BadRequest(["Restaurant name must be correct"]));
    //     if (!restaurantRepositoryService.RestaurantExists((int)id!))
    //         return NotFound(httpResponseJson.NotFound("Restaurant not found"));
    //     if (restaurantRepositoryService.RestaurantExists(name))
    //         return BadRequest(httpResponseJson.BadRequest(["Restaurant with this name already exists"]));
    //     if (restaurantRepositoryService.CheckIfOwner((int)id!, int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!)!))
    //         return BadRequest(httpResponseJson.BadRequest(["You are not the owner of this restaurant"]));
    //     await restaurantRepositoryService.ChangeRestaurantName((int)id!, name);
    //     return Ok(httpResponseJson.Ok("Restaurant name changed"));
    // }
    //
    // // change desc
    // [Authorize(Roles = nameof(AccountTypeModel.RestaurantOwner))]
    // [HttpPost("ChangeDescription")]
    // public async Task<IActionResult> ChangeRestaurantDescription([Required] int? id, [Required] string description)
    // {
    //     if (!id.HasValue) return BadRequest(httpResponseJson.BadRequest(["Restaurant id must be correct"]));
    //     if (string.IsNullOrEmpty(description)) return BadRequest(httpResponseJson.BadRequest(["Restaurant description must be correct"]));
    //     if (!restaurantRepositoryService.RestaurantExists((int)id!))
    //         return NotFound(httpResponseJson.NotFound("Restaurant not found"));
    //     if (restaurantRepositoryService.CheckIfOwner((int)id!, int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!)!))
    //         return BadRequest(httpResponseJson.BadRequest(["You are not the owner of this restaurant"]));
    //     await restaurantRepositoryService.ChangeRestaurantDescription((int)id!, description);
    //     return Ok(httpResponseJson.Ok("Restaurant description changed"));
    // }
    //
    // [Authorize(Roles = nameof(AccountTypeModel.RestaurantOwner))]
    // [HttpPost("ChangeAddress")]
    // public async Task<IActionResult> ChangeRestaurantAddress([Required] int? id, [Required] string address)
    // {
    //     if (!id.HasValue) return BadRequest(httpResponseJson.BadRequest(["Restaurant id must be correct"]));
    //     if (string.IsNullOrEmpty(address)) return BadRequest(httpResponseJson.BadRequest(["Restaurant address must be correct"]));
    //     if (!restaurantRepositoryService.RestaurantExists((int)id!))
    //         return NotFound(httpResponseJson.NotFound("Restaurant not found"));
    //     if (restaurantRepositoryService.CheckIfOwner((int)id!, int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!)!))
    //         return BadRequest(httpResponseJson.BadRequest(["You are not the owner of this restaurant"]));
    //     await restaurantRepositoryService.ChangeRestaurantAddress((int)id!, address);
    //     return Ok(httpResponseJson.Ok("Restaurant address changed"));
    // }

    [Authorize(Roles = nameof(AccountTypeModel.RestaurantOwner))]
    [HttpPost("Modify")]
    public async Task<IActionResult> ModifyRestaurant(int? id, [FromBody] ModifyRestaurantModel model)
    {
        var newValue = model.NewValue;
        var valueToChange = model.Field;
        
        if (!id.HasValue) return BadRequest(httpResponseJson.BadRequest(["Restaurant id must be correct"]));
        if (string.IsNullOrEmpty(newValue)) return BadRequest(httpResponseJson.BadRequest(["Restaurant " + valueToChange + " must be correct"]));
        if (!restaurantRepositoryService.RestaurantExists((int)id!))
            return NotFound(httpResponseJson.NotFound("Restaurant not found"));
        if (!restaurantRepositoryService.CheckIfOwner((int)id!, int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!)!))
            return Unauthorized(httpResponseJson.Unauthorized(["You are not the owner of this restaurant"]));
        switch (valueToChange.ToLower())
        {
            case "name":
                if (restaurantRepositoryService.RestaurantExists(newValue))
                    return BadRequest(httpResponseJson.BadRequest(["Restaurant with this name already exists"]));
                await restaurantRepositoryService.ChangeRestaurantName((int)id!, newValue);
                break;
            case "description":
                await restaurantRepositoryService.ChangeRestaurantDescription((int)id!, newValue);
                break;
            case "address":
                await restaurantRepositoryService.ChangeRestaurantAddress((int)id!, newValue);
                break;
            case "imageUrl":
                await restaurantRepositoryService.ChangeRestaurantImageUrl((int)id!, newValue);
                break;
            case "email":
                await restaurantRepositoryService.ChangeRestaurantEmail((int)id!, newValue);
                break;
            case "website":
                await restaurantRepositoryService.ChangeRestaurantWebsite((int)id!, newValue);
                break;
            case "phoneNumber":
                await restaurantRepositoryService.ChangeRestaurantPhoneNumber((int)id!, newValue);
                break;
            case "category":
                if (!dbContext.Categories.Any(c => c.Id == int.Parse(newValue)))
                    return BadRequest(httpResponseJson.BadRequest(["Category with this id does not exist"]));
                await restaurantRepositoryService.ChangeRestaurantCategory((int)id!, int.Parse(newValue));
                break;
            default:
                return BadRequest(httpResponseJson.BadRequest(["Invalid value to change"]));
        }
        return Ok(httpResponseJson.Ok("Restaurant " + valueToChange + " changed"));
    }
    
    
    
    [Authorize(Roles = nameof(AccountTypeModel.RestaurantOwner))]
    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteRestaurant([Required] int? id)
    {
        Console.WriteLine(restaurantRepositoryService.CheckIfOwner((int)id!, int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!)!));

        if (!id.HasValue) return BadRequest(httpResponseJson.BadRequest(["Restaurant id must be correct"]));
        if (!restaurantRepositoryService.RestaurantExists((int)id!))
            return NotFound(httpResponseJson.NotFound("Restaurant not found"));
        if (!restaurantRepositoryService.CheckIfOwner((int)id!, int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!)!))
            return Unauthorized(httpResponseJson.Unauthorized(["You are not the owner of this restaurant"]));
        await restaurantRepositoryService.DeleteRestaurant((int)id!);
        return Ok(httpResponseJson.Ok("Restaurant deleted"));
    }
    
}