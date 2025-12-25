using System.Security.Claims;

namespace ToDoList.Utils
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var idClaim = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException("Claim NameIdentifier not found in the token.");
            if (int.TryParse(idClaim.Value, out int userId))
            {
                return userId;
            }
            throw new InvalidOperationException($"The value of the user ID claim ('{idClaim}') is in an invalid format.");
        }
    }
}