using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using TableSpot.Dto;
using TableSpot.Models;

namespace TableSpot.Services;

public class AuthService(AppDbContext dbContext)
{
    public async Task<bool> CheckSession(HttpContext context, ClaimsPrincipal user)
    {
        if (user.Identity?.IsAuthenticated != true) return false;
        var account = dbContext.Accounts.FirstOrDefault(u => u.Email == user.Identity.Name);
        if (account == null) return false;
        if (account.Id.ToString() != user.FindFirst(ClaimTypes.NameIdentifier)?.Value) return false;
        await SignInAsync(context, dbContext.Accounts.FirstOrDefault(u => u.Email == user.Identity.Name)!);
        return true;
    }
    
    public async Task SignInAsync(HttpContext context, AccountDto user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Email),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Role, user.AccountTypeId.ToString()),
        };
        
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        
        await context.SignInAsync(new ClaimsPrincipal(identity), new AuthenticationProperties
        {
            IsPersistent = true,
            AllowRefresh = true,
            ExpiresUtc = DateTimeOffset.Now.AddDays(7)
        });
    }
}