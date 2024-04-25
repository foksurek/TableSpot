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
    [HttpGet("GetAll")]
    public async Task<object> GetAll()
    {
        var category = await appDbContext.Categories.ToListAsync();
        return Ok(httpResponseJsonService.Ok(category.Select(c =>
            new {
                c.Id,
                c.Name
            })));
    }

    [HttpGet("GetById")]
    public async Task<object> GetById(string id)
    {
        if (string.IsNullOrEmpty(id))
            return BadRequest(httpResponseJsonService.BadRequest(["Id is required"]));
        
        var category = await appDbContext.Categories.FirstOrDefaultAsync(c => c.Id == int.Parse(id));
        if (category == null)
            return NotFound(httpResponseJsonService.NotFound("Category not found"));

        return Ok(httpResponseJsonService.Ok(new {
            category.Id,
            category.Name
        }));
    }
}