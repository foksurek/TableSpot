using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TableSpot.Contexts;

namespace TableSpot.Controllers;

[Route("api/[controller]")]
public class RestaurantController(AppDbContext _dbContext) : ControllerBase
{
    [HttpGet("GetAll")]
    public async Task<ActionResult> GetAllRestaurants(int limit = 10, int offset = 0)
    {
        // Check if limit and offset are greater than 0
        if (limit < 0 || offset < 0) return BadRequest("Limit and offset must be greater than 0");
        if (limit > 100) return BadRequest("Limit must be less than 100");
        
        var data = await _dbContext.Restaurants.Where(r => r.Id > offset).Take(limit).ToListAsync();
        
        return Ok(data);
    }
    
    [HttpGet("GetById")]
    public async Task<ActionResult> GetRestaurantById(int id)
    {
        var data = await _dbContext.Restaurants.FindAsync(id);
        if (data == null) return NotFound("Restaurant not found");
        
        return Ok(data);
    }
    
    [HttpGet("GetByName")]
    public async Task<ActionResult> GetRestaurantByName(string name)
    {
        if (string.IsNullOrEmpty(name)) return BadRequest("Name cannot be empty");
        
        var data = await _dbContext.Restaurants.Where(r => r.Name.Contains(name)).ToListAsync();
        if (data.Count == 0) return NotFound("Restaurant not found");
        
        return Ok(data);
    }
}