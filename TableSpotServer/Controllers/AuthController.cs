using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TableSpot.Contexts;
using TableSpot.Interfaces;
using TableSpot.Services;

namespace TableSpot.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(
    IHttpResponseJsonService httpResponseJsonService, 
    IPasswordService passwordService,
    IAccountRepositoryService accountRepository,
    AuthService authService
    ) : ControllerBase
{
    [HttpPost("Login")]
    public async Task<ActionResult> Login([FromBody] LoginModel model)
    {
        if (!ModelState.IsValid) return BadRequest(httpResponseJsonService.BadRequest(["Email and password must be correctly filled"]));
        var user = await accountRepository.GetAccount(model.Email);
        if (user == null)
            return Unauthorized(httpResponseJsonService.Unauthorized(["Email or password is invalid"]));
        if (!passwordService.VerifyPassword(model.Password, user.Password))
            return Unauthorized(httpResponseJsonService.Unauthorized(["Email or password is invalid"]));
        
        await authService.SignInAsync(HttpContext, user);
        
        return Ok(httpResponseJsonService.Ok("Login successful"));
    }
    
    [HttpPost("CreateAccount")]
    public async Task<ActionResult> CreateAccount([FromBody] LoginModel model)
    {
        if (!ModelState.IsValid) return BadRequest(httpResponseJsonService.BadRequest(["Email and password must be correctly filled"]));
        if (await accountRepository.AccountExists(model.Email))
            return BadRequest(httpResponseJsonService.BadRequest(["Email already exists"]));
        var password = passwordService.HashPassword(model.Password);
        var newUser = await accountRepository.CreateAccount(model.Email, password);
        return Ok(httpResponseJsonService.Ok($"Account with email {newUser!.Email} created"));
    }
    
    [HttpGet("CheckSession")]
    public async Task<ActionResult> CheckSession()
    {
        if (await authService.CheckSession(HttpContext, User))
            return Ok(httpResponseJsonService.Ok("User is authenticated"));
        return Unauthorized(httpResponseJsonService.Unauthorized(["User is not authenticated"]));
    }

    [Authorize]
    [HttpGet("GetAccountData")]
    public ActionResult GetAccountData()
    {
        var account = new
        {
            Email = User.Identity?.Name,
            Id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
        };
        return Ok(httpResponseJsonService.Ok(account));
    }

}