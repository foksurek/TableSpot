using Microsoft.EntityFrameworkCore;
using TableSpot.Dto;
using TableSpot.Interfaces;
using TableSpot.Models;

namespace TableSpot.Services;

public class MenuRepositoryService(AppDbContext dbContext) : IMenuRepositoryService
{
    public async Task<List<MenuDto>> GetMenuForRestaurant(int id, int limit, int offset)
    {
        return await dbContext.Menus
            .Where(m => m.RestaurantId == id)
            .Skip(offset)
            .Take(limit)
            .ToListAsync();
    }

    public async Task AddToMenu(int restaurantId, AddToMenuModel menu)
    {
        await dbContext.Menus.AddAsync(new MenuDto
        {
            RestaurantId = restaurantId,
            Name = menu.Name,
            Description = menu.Description,
            Price = menu.Price,
            ImageUrl = menu.ImageUrl
        });
        await dbContext.SaveChangesAsync();
    }
}