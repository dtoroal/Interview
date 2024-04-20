using System.Security.Cryptography;
using System.Text;

namespace TalentuInterview.Authenticator.Utilites;
public interface IPasswordHasher
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string passwordHash);
}

public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }

    public bool VerifyPassword(string password, string passwordHash)
    {
        string hashedProvidedPassword = HashPassword(password);
        return string.Equals(passwordHash, hashedProvidedPassword);
    }
}