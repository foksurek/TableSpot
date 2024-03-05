﻿using TableSpot.Dto;
using TableSpot.Models;

namespace TableSpot.Interfaces;

public interface IRestaurantRepositoryService
{
    public Task<RestaurantResponseModel?> GetRestaurantById(int id);
    public Task<List<RestaurantDto>> GetAllRestaurants(int limit, int offset);
    
    public Task<List<RestaurantResponseModel>> GetRestaurantsByName(string name, int limit, int offset);
    public Task<List<RestaurantResponseModel>> GetRestaurantsByCategory(int categoryId, int limit, int offset);
    
}