using Microsoft.AspNetCore.Authorization;
using ToDoList.Utils;
namespace TodoList.Api.Security;

public class IsOwnerHandler(IHttpContextAccessor httpContextAccessor) : AuthorizationHandler<IsOwnerRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsOwnerRequirement requirement)
    {
        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext == null) return Task.CompletedTask;

        if (httpContext.Request.RouteValues.TryGetValue("id", out var routeIdObj) && routeIdObj != null)
        {
            if (int.TryParse(routeIdObj.ToString(), out int routeId))
            {
                var userId = context.User.GetUserId();
                if (userId == routeId)
                {
                    context.Succeed(requirement);
                }
            }
        }

        return Task.CompletedTask;
    }
}