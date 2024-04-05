using Microsoft.EntityFrameworkCore;
using TableSpot.Dto;
using TableSpot.Interfaces;
using TableSpot.Models;

namespace TableSpot.Services;

public class RestaurantRepositoryService(AppDbContext dbContext) : IRestaurantRepositoryService
{
    //TODO: Fix response object to RestaurantDto
    
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
            .FirstOrDefaultAsync();
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
                Category = new
                {
                    Id = r.Category.Id,
                    Name = r.Category.Name
                }
            })
            .ToListAsync();

        return responseData;
    }
    
    public async Task<List<RestaurantResponseModel>> GetRestaurantsByNameOrDescription(string query, int limit, int offset)
    {
        var responseData = await dbContext.Restaurants
            .Include(r => r.Category)
            .Where(r => r.Name.Contains(query) || r.Description.Contains(query))
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
    
    public Task<List<RestaurantDto>> GetRestaurantsByOwner(int accountId, int limit, int offset)
    {
        return dbContext.Restaurants
            .Where(r => r.OwnerAccountId == accountId)
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

    public bool CheckIfOwner(int restaurantId, int accountId)
    {
        return dbContext.Restaurants.Where(r => r.Id == restaurantId).Select(r => r.OwnerAccountId).First() == accountId;
    }

    public Task DeleteRestaurant(int id)
    {
        var restaurant = dbContext.Restaurants.First(r => r.Id == id);
        dbContext.Restaurants.Remove(restaurant);
        return dbContext.SaveChangesAsync();
    }

    public Task ChangeRestaurantName(int id, string name)
    {
        var restaurant = dbContext.Restaurants.First(r => r.Id == id);
        restaurant.Name = name;
        return dbContext.SaveChangesAsync();
    }

    public Task ChangeRestaurantAddress(int id, string address)
    {
        var restaurant = dbContext.Restaurants.First(r => r.Id == id);
        restaurant.Address = address;
        return dbContext.SaveChangesAsync();
    }

    public Task ChangeRestaurantDescription(int id, string description)
    {
        var restaurant = dbContext.Restaurants.First(r => r.Id == id);
        restaurant.Description = description;
        return dbContext.SaveChangesAsync();
    }

    public Task ChangeRestaurantImageUrl(int id, string imageUrl)
    {
        var restaurant = dbContext.Restaurants.First(r => r.Id == id);
        restaurant.ImageUrl = imageUrl;
        return dbContext.SaveChangesAsync();
    }

    public Task ChangeRestaurantEmail(int id, string email)
    {
        var restaurant = dbContext.Restaurants.First(r => r.Id == id);
        restaurant.Email = email;
        return dbContext.SaveChangesAsync();
    }

    public Task ChangeRestaurantWebsite(int id, string website)
    {
        var restaurant = dbContext.Restaurants.First(r => r.Id == id);
        restaurant.Website = website;
        return dbContext.SaveChangesAsync();
    }

    public Task ChangeRestaurantPhoneNumber(int id, string phoneNumber)
    {
        throw new NotImplementedException();
    }

    public Task ChangeRestaurantCategory(int id, int categoryId)
    {
        throw new NotImplementedException();
    }
}