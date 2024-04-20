using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TalentuInterview.Authenticator.Contexts;
using TalentuInterview.Authenticator.Services;
using TalentuInterview.Authenticator.Utilites;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();


string? jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
string? jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(options =>
 {
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = jwtIssuer,
         ValidAudience = jwtIssuer,
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey ?? ""))
     };
 });

builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddSqlServer<SqlServerContext>(builder.Configuration.GetConnectionString("TalentuSqlServerConnection"));

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();


app.MapGet("/", () => "Hello Authentication Service!");


//app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
