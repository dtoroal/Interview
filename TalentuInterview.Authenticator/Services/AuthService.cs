using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TalentuInterview.Authenticator.Contexts;
using TalentuInterview.Authenticator.Models;
using TalentuInterview.Authenticator.Utilites;

namespace TalentuInterview.Authenticator.Services;

public class AuthService: IAuthService
{
    private readonly SqlServerContext _dbContext;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IConfiguration _config;

    public AuthService(
        SqlServerContext dbContext, 
        IPasswordHasher passwordHasher, 
        IConfiguration config)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _config = config;
    }

    public async Task<User?> RegisterUser(string email, string password)
    {
        // Check if username already exists
        User? user = await _dbContext.User.FirstOrDefaultAsync(u => u.Email == email);
        if (user != null)
        {
            return null; // Username already taken
        }

        // Hash the password
        string passwordHash = _passwordHasher.HashPassword(password);

        // Save user to database
        _dbContext.User.Add(new User { Email = email, PasswordHash = passwordHash });
        await _dbContext.SaveChangesAsync();

        User newUser = new() { Email = email, PasswordHash = passwordHash };

        return newUser; // Registration successful
    }

    public async Task<User?> AuthenticateUser(string email, string password)
    {
        // Find user by username
        var user = await _dbContext.User.FirstOrDefaultAsync(u => u.Email == email);
        if (user != null && _passwordHasher.VerifyPassword(password, user.PasswordHash))
        {
            return user; // Authentication successful
        }
        return null; // Authentication failed
    }

    public string SetJWTToken(string email)
    {
        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? ""));
        SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        Claim[] claims =
        [
           new Claim(ClaimTypes.Email, email),
        ];

        JwtSecurityToken Sectoken = new(_config["Jwt:Issuer"],
          _config["Jwt:Issuer"],
          claims,
          expires: DateTime.Now.AddMinutes(120),
          signingCredentials: credentials);

        string token = new JwtSecurityTokenHandler().WriteToken(Sectoken);
        return token;
    }
}

public interface IAuthService
{
    Task<User?> RegisterUser(string email, string password);
    Task<User?> AuthenticateUser(string email, string password);
    string SetJWTToken(string email);
}