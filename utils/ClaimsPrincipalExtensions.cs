using System.Security.Claims;

namespace ToDoList.Utils
{
    /// <summary>
    /// Provides extension methods for the ClaimsPrincipal class, primarily to simplify 
    /// the retrieval of user identity information from JWT claims.
    /// </summary>
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Retrieves the authenticated user's ID (as an integer) from the security token claims.
        /// It expects the user ID to be stored in the standard NameIdentifier claim.
        /// </summary>
        /// <param name="claimsPrincipal">The ClaimsPrincipal instance, typically from HttpContext.User.</param>
        /// <returns>The authenticated user's ID.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the NameIdentifier claim is missing or if its value is not a valid integer.
        /// </exception>
        public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            // Attempt to find the value of the standard claim used for user unique ID.
            var idClaim = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(idClaim))
            {
                // Identity error: The token is present and authenticated, but lacks the necessary user identifier.
                throw new InvalidOperationException("Claim NameIdentifier not found in the token.");
            }

            // Attempt to parse the claim value (which is always a string) into the required integer type.
            if (int.TryParse(idClaim, out int userId))
            {
                return userId;
            }

            // Format error: The claim value exists but cannot be converted to an integer.
            throw new InvalidOperationException($"The value of the user ID claim ('{idClaim}') is in an invalid format.");
        }
    }
}