using Microsoft.EntityFrameworkCore;
using TableSpot.Contexts;
using TableSpot.Dto;
using TableSpot.Interfaces;

namespace TableSpot.Services;

public class AccountRepositoryService(AppDbContext dbContext) : IAccountRepositoryService
{
    public async Task<UserDto?> GetUser(string email)
    {
        return (await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email));
    }
    
    
}