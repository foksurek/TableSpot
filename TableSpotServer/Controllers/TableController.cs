using Microsoft.AspNetCore.Mvc;
using TableSpot.Attributes;
using TableSpot.Models;

namespace TableSpot.Controllers;

[ApiController]
[Route("Api/[controller]")]
[RAuthorization(AccountTypeModel.Employee)]
public class TableController : ControllerBase
{
    //TODO: Implement TableController
    [HttpGet("Test")]
    public IActionResult Test ()
    {
        return Ok("Test");
    }
}