using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TableSpot.Dto;
using TableSpot.Interfaces;
using TableSpot.Models;

namespace TableSpot.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = nameof(AccountTypeModel.RestaurantOwner))]
public class EmployeeController(
    IEmployeeRepositoryService employeeRepositoryService,
    IHttpResponseJsonService httpResponseJsonService,
    IAccountRepositoryService accountRepositoryService,
    IPasswordService passwordService
) : ControllerBase
{
    [HttpGet("GetEmployeesForRestaurant")]
    public async Task<IActionResult> GetEmployeesForRestaurant(int restaurantId, int limit = 20, int offset = 0)
    {
        var data = await employeeRepositoryService.GetAllEmployeesFromRestaurant(restaurantId, limit, offset);
        var responseData = data.Select(e => new EmployeeResponseModel
        {
            Name = e.Account.Name,
            Surname = e.Account.Surname,
            Email = e.Account.Email,
        }).ToList();

        return Ok(httpResponseJsonService.Ok(responseData));
    }
    
    //TODO: Verify security
    
    [HttpPost("AddEmployeeToRestaurant")]
    public async Task<IActionResult> AddEmployeeToRestaurant([FromBody] AddEmployeeModel model)
    {
        var accountId = model.AccountId;
        var restaurantId = model.RestaurantId;
        if (!await accountRepositoryService.EmployeeExist(accountId))
            return BadRequest(httpResponseJsonService.BadRequest(["Employee does not exist"]));
        if (employeeRepositoryService.CheckIfEmployeeBelongsToRestaurant(accountId, restaurantId))
            return BadRequest(httpResponseJsonService.BadRequest(["Employee already belongs to this restaurant"]));
        await employeeRepositoryService.AddEmployeeToRestaurant(restaurantId, accountId);
        return Ok(httpResponseJsonService.Ok("Employee added to restaurant successfully"));
    }
    
    [HttpPost("CreateAndAddEmployeeToRestaurant")]
    public async Task<IActionResult> CreateAndAddEmployeeToRestaurant(CreateEmployeeModel model)
    {
        var account = new AccountDto
        {
            Name = model.Name,
            Surname = model.Surname,
            Email = model.Email,
            Password = passwordService.HashPassword(Random.Shared.Next(100000, 999999).ToString()),
            AccountTypeId = 2
        }; 
        var submittedAccount = await accountRepositoryService.CreateAccount(account);
        if (!await accountRepositoryService.EmployeeExist(submittedAccount!.Id))
            return BadRequest(httpResponseJsonService.BadRequest(["Employee does not exist"]));
        if (employeeRepositoryService.CheckIfEmployeeBelongsToRestaurant(submittedAccount!.Id, model.RestaurantId))
            return BadRequest(httpResponseJsonService.BadRequest(["Employee already belongs to this restaurant"]));
        await employeeRepositoryService.AddEmployeeToRestaurant(model.RestaurantId, submittedAccount!.Id);
        return Ok(httpResponseJsonService.Ok("Employee created and added to restaurant successfully"));
    }
}