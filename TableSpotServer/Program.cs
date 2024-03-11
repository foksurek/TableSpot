using System.Text.Json;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TableSpot.Interfaces;
using TableSpot.Models;
using TableSpot.Services;

namespace TableSpot;
class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = actionContext =>
            {
                var errors = actionContext.ModelState
                    .Where(e => e.Value!.Errors.Count > 0)
                    .SelectMany(e => e.Value!.Errors.Select(er => er.ErrorMessage))
                    .ToList();
        
                var result = new
                {
                    Code = 400,
                    Message = "Validation errors occurred",
                    Details = errors
                };
        
                return new BadRequestObjectResult(result);
            };
        });
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.WithOrigins("http://localhost:5173")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
            options.AddPolicy("AllowAllMethods",
                builder => builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS"));
        });
        
        
        builder.Services.AddDbContext<AppDbContext>(o => o.UseMySQL(builder.Configuration.GetConnectionString("MySqlConnection")!));
        builder.Services.AddTransient<IRestaurantRepositoryService, RestaurantRepositoryService>();
        builder.Services.AddScoped<IHttpResponseJsonService, HttpResponseJsonService>();
        builder.Services.AddScoped<IMenuRepositoryService, MenuRepositoryService>();
        builder.Services.AddScoped<IEmployeeRepositoryService, EmployeeRepositoryService>();
        builder.Services.AddSingleton<IPasswordService,PasswordService>();
        builder.Services.AddScoped<IAccountRepositoryService, AccountRepositoryService>();
        builder.Services.AddScoped<AuthService>();

        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
        {
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromDays(1);
            options.Cookie.Name = "TableSpotAuthCookie";
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.Cookie.SameSite = SameSiteMode.Strict;
            options.Events.OnRedirectToAccessDenied = async context =>
            {
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(new 
                    { 
                        code = 401,
                        message = "Unauthorized",
                        details = new List<string> { "User is unauthorized" } 
                    }));
            };
            options.Events.OnRedirectToLogin = async context =>
            {
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(new 
                    { 
                        code = 401,
                        message = "Unauthorized",
                        details = new List<string> { "User is unauthorized" } 
                    }));
            };
        });
        
        var app = builder.Build();
        
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseExceptionHandler(error =>
        {
            error.Run(async context =>
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(new 
                    { 
                        code = 500,
                        message = "Internal server error",
                        details = new List<string> { "Something went wrong" } 
                    }));
            });
        });
        
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors();
        app.MapDefaultControllerRoute();
        
        app.Run();
    }
}