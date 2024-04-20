using Microsoft.AspNetCore.Mvc;
using TalentuInterview.Employees.Services;

namespace TalentuInterview.Employees.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployController : Controller
{
    readonly IEmployService employService;

    public EmployController(IEmployService service)
    {
        employService = service;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(employService.Get());
    }
}
