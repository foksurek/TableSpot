using TableSpot.Dto;

namespace TableSpot.Interfaces;

public interface IAccountRepositoryService
{
    public Task<UserDto?> GetUser(string email);
}