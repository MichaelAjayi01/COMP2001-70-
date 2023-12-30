using System;
using System.IdentityModel.Tokens.Jwt;

public static class JwtUtils
{
    public static void PrintTokenClaims(string token)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jsonToken != null)
            {
                Console.WriteLine("Token Claims:");

                foreach (var claim in jsonToken.Claims)
                {
                    Console.WriteLine($"{claim.Type}: {claim.Value}");
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
