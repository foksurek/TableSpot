using Microsoft.AspNetCore.Mvc;
using TableSpot.Contexts;
using TableSpot.Interfaces;
using TableSpot.Services;

namespace TableSpot.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController(
    IHttpResponseJsonService httpResponseJsonService, 
    IPasswordService passwordService,
    IAccountRepositoryService accountRepository
    ) : ControllerBase
{
    [HttpPost("Login")]
    public async Task<ActionResult> Login([FromBody] LoginModel model)
    {
        if (!ModelState.IsValid) return BadRequest(httpResponseJsonService.BadRequest(["Email and password must be correctly filled"]));
        Console.WriteLine(passwordService.HashPassword("testtest"));
        var user = await accountRepository.GetUser(model.Email);
        if (user == null)
            return Unauthorized(httpResponseJsonService.Unauthorized(["Email or password is invalid"]));
        if (!passwordService.VerifyPassword(model.Password, user.Password))
            return Unauthorized(httpResponseJsonService.Unauthorized(["Email or password is invalid"]));

        
        return Ok(httpResponseJsonService.Ok("Login successful"));
    }
}