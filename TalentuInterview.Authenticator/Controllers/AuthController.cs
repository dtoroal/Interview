using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using TalentuInterview.Authenticator.Models;
using TalentuInterview.Authenticator.Services;

namespace TalentuInterview.Authenticator.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly IAuthService _authenticationService;

    public AuthController(IAuthService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok("Funcionó");
    }

    [HttpPost]
    [Route("signup")]
    public IActionResult SignUp([FromBody] LoginRequest loginRequest)
    {
        Employee? newUser = _authenticationService.RegisterUser(loginRequest.Email, loginRequest.Password).Result;

        if (newUser != null)
        {
            string token = _authenticationService.SetJWTToken(newUser.Email);
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
        Employee? user = _authenticationService.AuthenticateUser(loginRequest.Email, loginRequest.Password).Result;

        if (user != null)
        {
            string token = _authenticationService.SetJWTToken(user.Email);
            return Ok(token);
        } else
        {
            return BadRequest("Wrong credetials");
        }
    }

}
