using Microsoft.AspNetCore.Authorization;

namespace TodoList.Api.Security;

public class IsOwnerRequirement : IAuthorizationRequirement { }