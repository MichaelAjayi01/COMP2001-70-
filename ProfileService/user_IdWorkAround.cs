using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

public static class JwtUtils
{
    public static string user_id_value = "-1"; // Marked as static

    public static void PrintTokenClaims(string token)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jsonToken != null)
            {
                var firstClaim = jsonToken.Claims.FirstOrDefault();

                if (firstClaim != null)
                {
                    user_id_value = firstClaim.Value; // Accessed in a static method
                }
                else
                {
                    Console.WriteLine("No claims found in the token");
                }
            }
            else
            {
                Console.WriteLine("Invalid JWT token format");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error decoding token: {ex.Message}");
        }
    }
}
