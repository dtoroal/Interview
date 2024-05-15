using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Interview.Authenticator.Services;
using Interview.Authenticator.Models;

namespace Interview.Authenticator.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly IAuthService _authenticationService;

    public AuthController(IAuthService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost]
    [Route("signup")]
    public IActionResult SignUp([FromBody] Employee user)
    {
        string? token = _authenticationService.RegisterUser(user).Result;

        if (string.IsNullOrEmpty(token))
        {
            return Ok(token);
        }
        else
        {
            return BadRequest("Could not create user");
        }
    }

    [HttpPost]
    [Route("login")]
    public IActionResult Login([FromBody] LoginRequest loginRequest)
    {
        string? token = _authenticationService.AuthenticateUser(loginRequest.Email, loginRequest.Password).Result;

        if (!string.IsNullOrEmpty(token))
        {
            return Ok(token);
        } else
        {
            return BadRequest("Wrong credetials");
        }
    }

}
