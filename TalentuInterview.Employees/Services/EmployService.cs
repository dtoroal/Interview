using TalentuInterview.Employees.Contexts;
using TalentuInterview.Employees.Models;

namespace TalentuInterview.Employees.Services;

public class EmployService : IEmployeeService
{

    readonly SqlServerContext context;

    public EmployService(SqlServerContext dbContext)
    {
        context = dbContext;
    }

    public IEnumerable<Employee> Get()
    {

        return context.Employees;
    }


    public Employee? Get(string? id)
    {
        if (id != null)
        {
            return context.Employees.FirstOrDefault(e => e.Id == Guid.Parse(id));
        }
        else
        {
            return null;
        }
    }

}

public interface IEmployeeService
{
    IEnumerable<Employee> Get();
    Employee? Get(string id);
}