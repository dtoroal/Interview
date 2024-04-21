using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using TalentuInterview.Employees.Models;
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

    [HttpGet("{id?}")]
    public IActionResult Get(string? id)
    {
        if (id == null)
        {
        return Ok(_employeeService.Get());
        } else
        {
            return Ok(_employeeService.Get(id));
        }
    }

    [HttpPut]
    [Route("update")]
    public IActionResult Put([FromBody] Object employee)
    {
        var json = Newtonsoft.Json.JsonConvert.DeserializeObject<Employee>(employee.ToString());
        bool response = _employeeService.Update(json);
        if (response)
        {
            return Ok(response);
        } else
        {
            return BadRequest(response);
        }
    }
}
