using Microsoft.AspNetCore.Mvc;
using TalentuInterview.Employees.Services;

namespace TalentuInterview.Employees.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : Controller
{
    readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_employeeService.Get());
    }
}
