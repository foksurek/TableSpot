using Microsoft.EntityFrameworkCore;
using TableSpot.Dto;
using TableSpot.Interfaces;
using TableSpot.Models;

namespace TableSpot.Services;

public class RestaurantRepositoryService(AppDbContext dbContext) : IRestaurantRepositoryService
{
    public async Task<List<RestaurantDto>> GetAllRestaurants(int limit, int offset)
    {

        return await dbContext.Restaurants
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
    }


    public async Task<RestaurantResponseModel?> GetRestaurantById(int id)
    {
        var data = await dbContext.Restaurants
            .Include(r => r.Category)
            .Where(r => r.Id == id)
            .Select(r => new RestaurantResponseModel()
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
            .FirstAsync();
        return data;
    }
    
    public async Task<List<RestaurantResponseModel>> GetRestaurantsByName(string name, int limit, int offset)
    {
        var responseData = await dbContext.Restaurants
            .Include(r => r.Category)
            .Where(r => r.Name.Contains(name))
            .Skip(offset)
            .Take(limit)
            .Select(r => new RestaurantResponseModel()
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
    
    
    public async Task<List<RestaurantResponseModel>> GetRestaurantsByCategory(int categoryId, int limit, int offset)
    {
        var responseData = await dbContext.Restaurants
            .Include(r => r.Category)
            .Where(r => r.Category.Id == categoryId)
            .Skip(offset)
            .Take(limit)
            .Select(r => new RestaurantResponseModel()
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

        return responseData;
    }
    
    
    public Task CreateRestaurant(RestaurantDto restaurant)
    {
        dbContext.Restaurants.Add(restaurant);
        return dbContext.SaveChangesAsync();
    }
    
    public bool RestaurantExists(int id)
    {
        return dbContext.Restaurants.Any(r => r.Id == id);
    }
    
    public bool RestaurantExists(string name)
    {
        return dbContext.Restaurants.Any(r => r.Name == name);
    }
}