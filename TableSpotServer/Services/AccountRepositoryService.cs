using Microsoft.EntityFrameworkCore;
using TableSpot.Dto;
using TableSpot.Interfaces;
using TableSpot.Models;

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
    
    public async Task<AccountDto?> CreateAccount(AccountDto account)
    {
        dbContext.Accounts.Add(account);
        await dbContext.SaveChangesAsync();
        return account;
    }
    
    public async Task<bool> AccountExists(string email)
    {
        return (await dbContext.Accounts.AnyAsync(u => u.Email.ToLower() == email.ToLower()));
    }
    
    
}