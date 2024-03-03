using TableSpot.Dto;

namespace TableSpot.Interfaces;

public interface IRestaurantService
{
    public Task<RestaurantDto?> GetRestaurantById(int id);
    public Task<List<RestaurantDto>> GetAllRestaurants(int limit, int offset);
    
    public Task<List<RestaurantDto>> GetRestaurantsByName(string name, int limit, int offset);
    public Task<List<RestaurantDto>> GetRestaurantsByCategory(int categoryId, int limit, int offset);
    
}