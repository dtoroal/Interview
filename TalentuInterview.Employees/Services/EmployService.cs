using TalentuInterview.Employees.Contexts;
using TalentuInterview.Employees.Models;

namespace TalentuInterview.Employees.Services;

public class EmployService: IEmployeeService
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

}

public interface IEmployeeService
{
    IEnumerable<Employee> Get();
}