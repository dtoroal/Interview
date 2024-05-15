using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Interview.Authenticator.Contexts;
using Interview.Authenticator.Models;
using Interview.Authenticator.Utilites;
using Microsoft.AspNetCore.Identity.Data;

namespace Interview.Authenticator.Services;

public class AuthService : IAuthService
{
    private readonly SqlServerContext _employeeDbContext;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IConfiguration _config;

    public AuthService(
        SqlServerContext dbContext,
        IPasswordHasher passwordHasher,
        IConfiguration config)
    {
        _employeeDbContext = dbContext;
        _passwordHasher = passwordHasher;
        _config = config;
    }

    public async Task<string?> RegisterUser(string email, string password)
    {
        try
        {
            // Check if username already exists
            Employee? user = await _employeeDbContext.Employee.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null)
            {
                return null; // Username already taken
            }

            // Hash the password
            string passwordHash = _passwordHasher.HashPassword(password);

            // Save user to database
            _employeeDbContext.Employee.Add(new Employee { Email = email, HashPassword = passwordHash });
            await _employeeDbContext.SaveChangesAsync();

            return SetJWTToken(email);
        }
        catch (Exception)
        {

            return null;
        }
    }

    public async Task<string?> AuthenticateUser(string email, string password)
    {
        Employee? user = await _employeeDbContext.Employee.FirstOrDefaultAsync(u => u.Email == email);
        if (user != null && _passwordHasher.VerifyPassword(password, user.HashPassword))
        {
            return SetJWTToken(user.Email);
        }
        return null;
    }

    public string SetJWTToken(string email)
    {

        SymmetricSecurityKey secretKey = new (Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? ""));
        SigningCredentials signingCredentials = new(secretKey, SecurityAlgorithms.HmacSha256);

        List<Claim> claims =
        [
            new Claim(JwtRegisteredClaimNames.Email, email),
        ];
        var tokenOptions = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"] ?? "",
            claims: claims,
            expires: DateTime.Now.AddMinutes(100),
            signingCredentials: signingCredentials
        );
        string tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        return tokenString;
    }
}

public interface IAuthService
{
    /// <summary>
    /// Register a new user
    /// </summary>
    /// <param name="email">New user email</param>
    /// <param name="password">New password</param>
    /// <returns>New employee data</returns>
    Task<string?> RegisterUser(string email, string password);

    /// <summary>
    /// User authentication
    /// </summary>
    /// <param name="email">New user email</param>
    /// <param name="password">New password</param>
    /// <returns>Authentication token</returns>
    Task<string?> AuthenticateUser(string email, string password);
}