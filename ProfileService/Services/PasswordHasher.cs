// Assuming this is in a file named PasswordHasher.cs

using System;
using System.Security.Cryptography;
using System.Text;

public static class PasswordHasher
{
    private const int SaltSize = 16; // Adjust the salt size as needed

    public static string HashPassword(string password)
    {
        // Generate a random salt
        byte[] salt = GenerateSalt();

        // Convert the password string to a byte array
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

        // Concatenate the password byte array with the salt
        byte[] combinedBytes = new byte[passwordBytes.Length + salt.Length];
        Buffer.BlockCopy(passwordBytes, 0, combinedBytes, 0, passwordBytes.Length);
        Buffer.BlockCopy(salt, 0, combinedBytes, passwordBytes.Length, salt.Length);

        using (var sha256 = SHA256.Create())
        {
            // Compute the hash value of the combined byte array
            byte[] hashBytes = sha256.ComputeHash(combinedBytes);

            // Convert the hash byte array to a hexadecimal string
            string hashString = BitConverter.ToString(hashBytes).Replace("-", "");

            // Include the salt in the final hash string (for verification)
            return $"{hashString}.{Convert.ToBase64String(salt)}";
        }
    }

private static byte[] GenerateSalt()
{
    // Generate a random salt
    byte[] salt = new byte[SaltSize];
    RandomNumberGenerator.Fill(salt);
    return salt;
}
}
