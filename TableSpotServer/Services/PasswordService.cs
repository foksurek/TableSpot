using System.Security.Cryptography;
using System.Text;
using TableSpot.Interfaces;

namespace TableSpot.Services;

public class PasswordService : IPasswordService
{
    private static string ComputeSha256Hash(string data)
    {
        var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(data));
        return BitConverter.ToString(hashedBytes);
    }


    public bool VerifyPassword(string rawPassword, string hashedPassword)
    {
        var password = ComputeSha256Hash(rawPassword);
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }

    public string HashPassword(string password)
    {
        var hashedPassword = ComputeSha256Hash(password);
        var bcryptHash = BCrypt.Net.BCrypt.HashPassword(hashedPassword);
        
        return bcryptHash;
    }
}