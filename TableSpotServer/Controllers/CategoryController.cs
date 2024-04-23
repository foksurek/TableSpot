using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TableSpot.Interfaces;
using TableSpot.Models;

namespace TableSpot.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class CategoryController(
    AppDbContext appDbContext,
    IHttpResponseJsonService httpResponseJsonService
) : ControllerBase
{
    [HttpGet]
    public async Task<object> Get()
    {
        var category = await appDbContext.Categories.ToListAsync();
        return Ok(httpResponseJsonService.Ok(category.Select(c =>
            new {
                c.Name,
                c.Id
            })));
    }
}