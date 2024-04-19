using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TalentuInterview.Employees.Services;

namespace TalentuInterview.Employees.Controllers;

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
