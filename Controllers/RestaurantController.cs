using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TableSpot.Contexts;
using TableSpot.Interfaces;
using TableSpot.Services;

namespace TableSpot.Controllers;

[Route("api/[controller]")]
public class RestaurantController(
    AppDbContext dbContext, 
    IRestaurantService restaurantService
) : ControllerBase
{
    
    private readonly AppDbContext _dbContext = dbContext;

    [HttpGet("GetAll")]
    public async Task<ActionResult> GetAllRestaurants(int limit = 10, int offset = 0)
    {
        if (limit < 0 || offset < 0) return BadRequest("Limit and offset must be greater than 0");
        if (limit > 100) return BadRequest("Limit must be less than 100");
        
        return Ok(await restaurantService.GetAllRestaurants(limit, offset));
    }
    
    [HttpGet("GetById")]
    public async Task<ActionResult> GetRestaurantById([Required]int id)
    {
        var data = await restaurantService.GetRestaurantById(id);
        if (data == null) return NotFound("Restaurant not found");
        
        return Ok(data);
    }
    
    [HttpGet("GetByName")]
    public async Task<ActionResult> GetRestaurantByName([Required]string name, int limit = 10, int offset = 0)
    {
        var data = await restaurantService.GetRestaurantsByName(name, limit, offset);
        
        return Ok(data);
    }
    
    [HttpGet("GetByCategory")]
    public async Task<ActionResult> GetRestaurantByName([Required]int categoryId, int limit = 10, int offset = 0)
    {
        var data = await restaurantService.GetRestaurantsByCategory(categoryId, limit, offset);
        
        return Ok(data);
    }
}