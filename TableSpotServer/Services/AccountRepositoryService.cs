using Microsoft.EntityFrameworkCore;
using TableSpot.Contexts;
using TableSpot.Dto;
using TableSpot.Interfaces;

namespace TableSpot.Services;

public class AccountRepositoryService(AppDbContext dbContext) : IAccountRepositoryService
{
    public async Task<AccountDto?> GetAccount(string email)
    {
        return (await dbContext.Accounts.FirstOrDefaultAsync(u => u.Email == email));
    }
    
    public async Task<AccountDto?> GetAccount(int id)
    {
        return (await dbContext.Accounts.FirstOrDefaultAsync(u => u.Id == id));
    }
    
    public async Task<AccountDto?> CreateAccount(string email, string password)
    {
        var account = new AccountDto
        {
            Email = email,
            Password = password
        };
        dbContext.Accounts.Add(account);
        await dbContext.SaveChangesAsync();
        return account;
    }
    
    public async Task<bool> AccountExists(string email)
    {
        return (await dbContext.Accounts.AnyAsync(u => u.Email.ToLower() == email.ToLower()));
    }
    
    
}