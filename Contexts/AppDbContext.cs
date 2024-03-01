using Microsoft.EntityFrameworkCore;
using TableSpot.Dto;

namespace TableSpot.Contexts;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<RestaurantDto> Restaurants { get; set; } = null!;
};