using TableSpot.Dto;
using TableSpot.Models;

namespace TableSpot.Interfaces;

public interface IRestaurantRepositoryService
{
    public Task<RestaurantResponseModel?> GetRestaurantById(int id);
    public Task<List<RestaurantDto>> GetAllRestaurants(int limit, int offset);
    
    public Task<List<RestaurantResponseModel>> GetRestaurantsByName(string name, int limit, int offset);
    public Task<List<RestaurantResponseModel>> GetRestaurantsByCategory(int categoryId, int limit, int offset);
    public Task CreateRestaurant(RestaurantDto restaurant);
    public bool RestaurantExists(int id);
    public bool RestaurantExists(string name);
    public bool CheckIfOwner(int restaurantId, int accountId);

    public Task DeleteRestaurant(int id);
    public Task ChangeRestaurantName(int id, string name);
    public Task ChangeRestaurantAddress(int id, string address);
    public Task ChangeRestaurantDescription(int id, string description);
    public Task ChangeRestaurantImageUrl(int id, string imageUrl);
    public Task ChangeRestaurantEmail(int id, string email);
    public Task ChangeRestaurantWebsite(int id, string website);
    public Task ChangeRestaurantPhoneNumber(int id, string phoneNumber);
    public Task ChangeRestaurantCategory(int id, int categoryId);
    





}