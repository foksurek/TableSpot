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
    
        var responseData = await _dbContext.Restaurants
            .Where(r => r.Id > offset)
            .Take(limit)
            .Select(r => new
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
            })
            .ToListAsync();

        return Ok(responseData);
    }
    
    [HttpGet("GetById")]
    public async Task<ActionResult> GetRestaurantById(int id)
    {
        var data = await _dbContext.Restaurants.Include(r => r.Category)
            .Where(r => r.Id == id).FirstOrDefaultAsync();
        if (data == null) return NotFound("Restaurant not found");
        
        var responseData = new
        {
            Id = data.Id,
            Name = data.Name,
            Address = data.Address,
            Description = data.Description,
            ImageUrl = data.ImageUrl,
            Email = data.Email,
            Website = data.Website,
            PhoneNumber = data.PhoneNumber,
            Category = new
            {
                Id = data.Category.Id,
                Name = data.Category.Name
            }
        };
        
        
        
        return Ok(responseData);
    }
    
    [HttpGet("GetByName")]
    public async Task<ActionResult> GetRestaurantByName(string name)
    {
        if (string.IsNullOrEmpty(name)) return BadRequest("Name cannot be empty");
        
        var data = await _dbContext.Restaurants.
            Include(r => r.Category).
            Where(r => r.Name.Contains(name)).
            Select(r => new
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
            
            }).
            ToListAsync();
        if (data.Count == 0) return NotFound("Restaurant not found");
        
        return Ok(data);
    }
}