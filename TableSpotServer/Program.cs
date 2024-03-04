using Microsoft.EntityFrameworkCore;
using TableSpot.Contexts;
using TableSpot.Interfaces;
using TableSpot.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(o => o.UseMySQL(builder.Configuration.GetConnectionString("MySqlConnection")!));
builder.Services.AddTransient<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<IHttpResponseJsonService, HttpResponseJsonService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapDefaultControllerRoute();

app.Run();