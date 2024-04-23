using Azure;
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
        }
        else
        {
            return Ok(_employeeService.Get(id));
        }
    }

    [HttpPut]
    [Route("update")]
    public IActionResult Put([FromBody] EmployeeRequest employee)
    {
        bool response = _employeeService.Update(employee);
        if (response)
        {
            return Ok(response);
        }
        else
        {
            return BadRequest(response);
        }
    }

    [HttpPost]
    [Route("post")]
    public IActionResult Post([FromBody] EmployeeRequest employee)
    {
        try
        {
            bool response = _employeeService.Post(employee);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{email}")]
    [Route("delete/{email}")]
    public IActionResult Delete(string? email)
    {
        try
        {
            bool response = _employeeService.Delete(email);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
