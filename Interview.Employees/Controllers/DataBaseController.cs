using Microsoft.AspNetCore.Mvc;
using Interview.Employees.Contexts;

namespace Interview.Employees.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DataBaseController : Controller
{
    readonly SqlServerContext sqlServerContext;

    public DataBaseController(SqlServerContext dbContext)
    {
        sqlServerContext = dbContext;
    }

    [HttpGet]
    [Route("createdb")]
    public IActionResult DatabaseCreated()
    {
        if (sqlServerContext.Database.EnsureCreated())
        {
            return Ok("Database created");
        } else
        {
            return BadRequest("Database already created");
        }

    }

}
