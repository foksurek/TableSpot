using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TableSpot.Contexts;
using TableSpot.Dto;
using TableSpot.Interfaces;


namespace TableSpot.Controllers;

[Route("api/[controller]")]
public class RestaurantController(
    IRestaurantService restaurantService,
    IHttpResponseJsonService httpResponseJson
) : ControllerBase
{
    [HttpGet("GetAll")]
    public async Task<ActionResult> GetAllRestaurants(int limit = 10, int offset = 0)
    {
        List<string> details = [];
        if (limit < 0 || offset < 0) details.Add("Limit and offset must be greater than 0");
        if (limit > 100) details.Add("Limit must be less than 100");
        if (details.Count > 0) return BadRequest(httpResponseJson.BadRequest(details));
        
        return Ok(httpResponseJson.Ok(await restaurantService.GetAllRestaurants(limit, offset)));
    }
    
    [HttpGet("GetById")]
    public async Task<ActionResult> GetRestaurantById([Required] int? id)
    {
        if (!id.HasValue) return BadRequest(httpResponseJson.BadRequest(["Restaurant id must be correct"]));
        var data = await restaurantService.GetRestaurantById((int)id!);
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
        var data = await restaurantService.GetRestaurantsByName(name, limit, offset);
        
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
        
        var data = await restaurantService.GetRestaurantsByCategory((int)categoryId!, limit, offset);
        
        return Ok(httpResponseJson.Ok(data));
    }
    
    
}