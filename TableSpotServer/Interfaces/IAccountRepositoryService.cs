using TableSpot.Dto;

namespace TableSpot.Interfaces;

public interface IAccountRepositoryService
{
    public Task<AccountDto?> GetAccount(string email);
    public Task<AccountDto?> GetAccount(int id);
    public Task<AccountDto?> CreateAccount(string email, string password);
    public Task<bool> AccountExists(string email);
    
}