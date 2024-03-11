using Microsoft.EntityFrameworkCore;
using TableSpot.Dto;
using TableSpot.Interfaces;
using TableSpot.Models;

namespace TableSpot.Services;

public class EmployeeRepositoryService(AppDbContext dbContext) : IEmployeeRepositoryService
{
    public bool CheckIfEmployeeExists(int accountId)
    {
        return dbContext.Employees.Any(e => e.AccountId == accountId);
    }

    public bool CheckIfEmployeeBelongsToRestaurant(int accountId, int restaurantId)
    {
        return dbContext.Employees.Any(e => e.AccountId == accountId && e.RestaurantId == restaurantId);
    }
    
    public async Task<List<EmployeeDto>> GetAllEmployeesFromRestaurant(int restaurantId, int limit, int offset)
    {
        return await dbContext.Employees
            .Where(e => e.RestaurantId == restaurantId)
            .Take(limit)
            .Skip(offset)
            .ToListAsync();
    }

    public Task AddEmployeeToRestaurant(int restaurantId, int accountId)
    {
        var employee = new EmployeeDto()
        {
            AccountId = accountId,
            RestaurantId = restaurantId
        };
        dbContext.Employees.Add(employee);
        return dbContext.SaveChangesAsync();
    }
}