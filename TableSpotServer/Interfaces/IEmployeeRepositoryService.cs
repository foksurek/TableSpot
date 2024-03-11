using TableSpot.Dto;

namespace TableSpot.Interfaces;

public interface IEmployeeRepositoryService
{
    public bool CheckIfEmployeeExists(int accountId);
    public bool CheckIfEmployeeBelongsToRestaurant(int accountId, int restaurantId);
    public Task<List<EmployeeDto>> GetAllEmployeesFromRestaurant(int restaurantId, int limit, int offset);
    public Task AddEmployeeToRestaurant(int restaurantId, int accountId);
    
    
}