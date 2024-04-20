using Microsoft.EntityFrameworkCore;
using TalentuInterview.Authenticator.Models;

namespace TalentuInterview.Authenticator.Contexts;

public class SqlServerContext : DbContext
{
    public DbSet<Employee> Employee { get; set; }

    public SqlServerContext(DbContextOptions<SqlServerContext> options) : base(options) { }

}
