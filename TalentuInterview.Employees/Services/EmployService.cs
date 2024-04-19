using TalentuInterview.Employees.Contexts;
using TalentuInterview.Employees.Models;

namespace TalentuInterview.Employees.Services;

public class EmployService: IEmployService
{

    readonly SqlServerContext context;

    public EmployService(SqlServerContext dbContext)
    {
        context = dbContext;
    }


    public IEnumerable<Employ> Get()
    {
        return context.Employs;
    }

}

public interface IEmployService
{
    IEnumerable<Employ> Get();
}

