﻿using Microsoft.EntityFrameworkCore;
using TableSpot.Dto;

namespace TableSpot.Models;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<RestaurantDto> Restaurants { get; set; } = null!;
    public DbSet<CategoryDto> Categories { get; set; } = null!;
    public DbSet<AccountDto> Accounts { get; set; } = null!;
    public DbSet<MenuDto> Menus { get; set; } = null!;
    public DbSet<OrderDto> Orders { get; set; } = null!;
    public DbSet<OrderElementDto> OrderElements { get; set; } = null!;
    public DbSet<EmployeeDto> Employees { get; set; } = null!;
};