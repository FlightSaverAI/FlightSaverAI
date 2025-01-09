using System.Security.Claims;

namespace FlightSaverApi.Helpers;

public static class ClaimsHelper
{
    /// <summary>
    /// Extracts the UserId from the claims in the HttpContext.
    /// </summary>
    /// <param name="user">ClaimsPrincipal containing the user's claims.</param>
    /// <returns>UserId as an integer if valid; throws an exception otherwise.</returns>
    /// <exception cref="UnauthorizedAccessException">Thrown if UserId claim is missing.</exception>
    /// <exception cref="InvalidOperationException">Thrown if UserId claim is invalid or not parsable.</exception>
    public static int GetUserIdFromClaims(ClaimsPrincipal user)
    {
        var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == "id");
        if (userIdClaim == null)
        {
            throw new UnauthorizedAccessException("UserId claim not found in token.");
        }

        if (!int.TryParse(userIdClaim.Value, out int parsedUserId))
        {
            throw new InvalidOperationException("Invalid UserId in token.");
        }

        return parsedUserId;
    }
}