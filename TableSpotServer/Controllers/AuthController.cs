﻿using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TableSpot.Dto;
using TableSpot.Interfaces;
using TableSpot.Models;
using TableSpot.Services;

namespace TableSpot.Controllers;

[ApiController]
[Route("Api/[controller]")]
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
        
        return Ok(httpResponseJsonService.Ok(new
        {
            Id = user.Id,
            Email = user.Email,
            AccountType = new
            {
                Id = user.AccountTypeId,
                Type = Enum.GetName(typeof(AccountTypeModel), user.AccountTypeId)
            }
        }));
    }
    
    [Authorize]
    [HttpGet("Logout")]
    public async Task<ActionResult> Logout()
    {
        await authService.SignOutAsync(HttpContext);
        return Ok(httpResponseJsonService.Ok("User logged out"));
    }
    
    [HttpPost("CreateAccount")]
    public async Task<ActionResult> CreateAccount([FromBody] CreateAccountModel model)
    {
        if (!ModelState.IsValid) 
            return BadRequest(httpResponseJsonService.BadRequest(["Email and password must be correctly filled"]));
        if (await accountRepository.AccountExists(model.Email))
            return BadRequest(httpResponseJsonService.BadRequest(["Email already exists"]));
        var password = passwordService.HashPassword(model.Password);
        var newUser = await accountRepository.CreateAccount(new AccountDto
        {
            Email = model.Email,
            Name = model.Name,
            Surname = model.Surname,
            Password = password,
            AccountTypeId = model.AccountTypeId
        });
        return Ok(httpResponseJsonService.Ok($"Account with email {newUser!.Email} created"));
    }
    
    [HttpGet("CheckSession")]
    public async Task<ActionResult> CheckSession()
    {
        if (await authService.CheckSession(HttpContext, User))
            return Ok(httpResponseJsonService.Ok("User is authenticated"));
        return Unauthorized(httpResponseJsonService.Unauthorized(["User is not authenticated"]));
    }
    
}