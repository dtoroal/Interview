using System.Security.Cryptography;
using System.Text;

namespace Interview.Authenticator.Utilites;
public interface IPasswordHasher
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string passwordHash);
}

public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        byte[] hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }

    public bool VerifyPassword(string password, string passwordHash)
    {
        string hashedProvidedPassword = HashPassword(password);
        return string.Equals(passwordHash, hashedProvidedPassword);
    }
}