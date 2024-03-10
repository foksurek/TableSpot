using TableSpot.Dto;
using TableSpot.Models;

namespace TableSpot.Interfaces;

public interface IMenuRepositoryService
{
    public Task<List<MenuDto>> GetMenuForRestaurant(int id, int limit, int offset);
    public Task AddToMenu(int restaurantId, AddToMenuModel menu);

}