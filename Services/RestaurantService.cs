using Microsoft.EntityFrameworkCore;
using TableSpot.Contexts;
using TableSpot.Dto;
using TableSpot.Interfaces;

namespace TableSpot.Services;

public class RestaurantService(AppDbContext _dbContext) : IRestaurantService
{
    public async Task<List<RestaurantDto>> GetAllRestaurants(int limit, int offset)
    {
        var responseData = await _dbContext.Restaurants
            .Include(r => r.Category)
            .Skip(offset)
            .Take(limit)
            .Select(r => new RestaurantDto
            {
                Id = r.Id,
                Name = r.Name,
                Address = r.Address,
                Description = r.Description,
                ImageUrl = r.ImageUrl,
                Email = r.Email,
                Website = r.Website,
                PhoneNumber = r.PhoneNumber,
                Category = new CategoryDto
                {
                    Id = r.Category.Id,
                    Name = r.Category.Name
                }
            })
            .ToListAsync();

        return responseData;
    }


    public async Task<RestaurantDto?> GetRestaurantById(int id)
    {
        var data = await _dbContext.Restaurants
            .Include(r => r.Category)
            .Where(r => r.Id == id)
            .Select(r => new RestaurantDto
            {
                Id = r.Id,
                Name = r.Name,
                Address = r.Address,
                Description = r.Description,
                ImageUrl = r.ImageUrl,
                Email = r.Email,
                Website = r.Website,
                PhoneNumber = r.PhoneNumber,
                Category = new CategoryDto
                {
                    Id = r.Category.Id,
                    Name = r.Category.Name
                }
            })
            .FirstOrDefaultAsync();
        return data;
    }
    
    public async Task<List<RestaurantDto>> GetRestaurantsByName(string name, int limit, int offset)
    {
        var responseData = await _dbContext.Restaurants
            .Include(r => r.Category)
            .Where(r => r.Name.Contains(name))
            .Skip(offset)
            .Take(limit)
            .Select(r => new RestaurantDto
            {
                Id = r.Id,
                Name = r.Name,
                Address = r.Address,
                Description = r.Description,
                ImageUrl = r.ImageUrl,
                Email = r.Email,
                Website = r.Website,
                PhoneNumber = r.PhoneNumber,
                Category = new CategoryDto
                {
                    Id = r.Category.Id,
                    Name = r.Category.Name
                }
            })
            .ToListAsync();

        return responseData;
    }
    
    
    public async Task<List<RestaurantDto>> GetRestaurantsByCategory(int categoryId, int limit, int offset)
    {
        var responseData = await _dbContext.Restaurants
            .Include(r => r.Category)
            .Where(r => r.Category.Id == categoryId)
            .Skip(offset)
            .Take(limit)
            .Select(r => new RestaurantDto
            {
                Id = r.Id,
                Name = r.Name,
                Address = r.Address,
                Description = r.Description,
                ImageUrl = r.ImageUrl,
                Email = r.Email,
                Website = r.Website,
                PhoneNumber = r.PhoneNumber,
                Category = new CategoryDto
                {
                    Id = r.Category.Id,
                    Name = r.Category.Name
                }
            })
            .ToListAsync();

        return responseData;
    }
}