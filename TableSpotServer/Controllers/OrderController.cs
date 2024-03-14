using Microsoft.AspNetCore.Mvc;
using TableSpot.Attributes;
using TableSpot.Models;

namespace TableSpot.Controllers;
[ApiController]
[Route("Api/[controller]")]
[RAuthorization(AccountTypeModel.Employee)]
public class OrderController : ControllerBase
{
    //TODO: Implement OrderController
}