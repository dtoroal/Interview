using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Interview.Authenticator.Contexts;
using Interview.Authenticator.Models;
using Interview.Authenticator.Utilites;

namespace Interview.Authenticator.Services;

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

    public async Task<Employee?> RegisterUser(string email, string password)
    {
        // Check if username already exists
        Employee? user = await _dbContext.Employee.FirstOrDefaultAsync(u => u.Email == email);
        if (user != null)
        {
            return null; // Username already taken
        }

        // Hash the password
        string passwordHash = _passwordHasher.HashPassword(password);

        // Save user to database
        _dbContext.Employee.Add(new Employee { Email = email, HashPassword = passwordHash });
        await _dbContext.SaveChangesAsync();

        Employee newUser = new() { Email = email, HashPassword = passwordHash };

        return newUser; // Registration successful
    }

    public async Task<Employee?> AuthenticateUser(string email, string password)
    {
        // Find user by username
        var user = await _dbContext.Employee.FirstOrDefaultAsync(u => u.Email == email);
        if (user != null && _passwordHasher.VerifyPassword(password, user.HashPassword))
        {
            return user; // Authentication successful
        }
        return null; // Authentication failed
    }

    public string SetJWTToken(string email)
    {

        SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? ""));
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
    Task<Employee?> RegisterUser(string email, string password);
    Task<Employee?> AuthenticateUser(string email, string password);
    string SetJWTToken(string email);
}