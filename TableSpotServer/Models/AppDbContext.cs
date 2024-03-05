using Microsoft.EntityFrameworkCore;
using TableSpot.Dto;

namespace TableSpot.Contexts;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<RestaurantDto> Restaurants { get; set; } = null!;
    public DbSet<CategoryDto> Categories { get; set; } = null!;
    public DbSet<AccountDto> Accounts { get; set; } = null!;
};