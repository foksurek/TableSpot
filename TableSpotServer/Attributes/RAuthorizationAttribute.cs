using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TableSpot.Models;

namespace TableSpot.Attributes;

public class RAuthorizationAttribute(AccountTypeModel requiredRole) : AuthorizeAttribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;
        if (!user.Identity!.IsAuthenticated)
        {
            context.Result = new UnauthorizedResult();
            return;
        }
        
        var userRole = (AccountTypeModel)Enum.Parse(typeof(AccountTypeModel), user.FindFirst(ClaimTypes.Role)!.Value);
        if (userRole >= requiredRole) return;
        context.Result = new ForbidResult();
        return;
    }
}