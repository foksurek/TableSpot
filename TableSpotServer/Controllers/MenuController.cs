﻿using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TableSpot.Dto;
using TableSpot.Interfaces;
using TableSpot.Models;

namespace TableSpot.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class MenuController(
    IMenuRepositoryService menuRepositoryService,
    IRestaurantRepositoryService restaurantRepositoryService,
    IHttpResponseJsonService httpResponseJsonService
    ) : ControllerBase
{
    [HttpGet("GetMenuForRestaurant")]
    public async Task<ActionResult<List<MenuDto>>> GetMenuForRestaurant(int restaurantId, int limit = 20, int offset = 0)
    {
        return await menuRepositoryService.GetMenuForRestaurant(restaurantId, limit, offset);
    }
    
    [HttpPost("AddToMenu")]
    [Authorize(Roles = nameof(AccountTypeModel.RestaurantOwner))]
    public async Task<ActionResult> AddToMenu([FromBody] AddToMenuModel menu)
    {
        if (!ModelState.IsValid) return BadRequest(httpResponseJsonService.BadRequest(["Menu item must be correctly filled"]));
        if (!restaurantRepositoryService.RestaurantExists(menu.RestaurantId)) 
            return BadRequest(httpResponseJsonService.BadRequest(["Restaurant does not exist"]));
        if (!restaurantRepositoryService.CheckIfOwner(menu.RestaurantId, int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!)!))
            return Unauthorized(httpResponseJsonService.Unauthorized(["You are not the owner of this restaurant"]));
        await menuRepositoryService.AddToMenu(menu.RestaurantId, menu);
        return Ok(httpResponseJsonService.Ok("Menu item added successfully"));
    }
}