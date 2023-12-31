using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

public static class JwtUtils
{
    public static string user_id_value = " "; // Marked as static

    public static void PrintTokenClaims(string token)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jsonToken != null)
            {
                Console.WriteLine("Token Claims:");
                var firstClaim = jsonToken.Claims.FirstOrDefault();

                if (firstClaim != null)
                {
                    Console.WriteLine($"{firstClaim.Type}: {firstClaim.Value}");
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
