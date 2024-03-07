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
[Route("api/[controller]")]
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
    
    [HttpGet("GetByName")]
    public async Task<ActionResult> GetRestaurantByName([Required] string name, int limit = 10, int offset = 0)
    {
        List<string> details = [];
        if (string.IsNullOrEmpty(name)) details.Add("Restaurant name must be correct");
        if (limit < 0 || offset < 0) details.Add("Limit and offset must be greater than 0");
        if (limit > 100) details.Add("Limit must be less than 100");
        if (details.Count > 0) return BadRequest(httpResponseJson.BadRequest(details));
        var data = await restaurantRepositoryService.GetRestaurantsByName(name, limit, offset);
        
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
    
    
}