using Microsoft.EntityFrameworkCore;
using Interview.Authenticator.Models;

namespace Interview.Authenticator.Contexts;

public class SqlServerContext : DbContext
{
    public DbSet<Employee> Employee { get; set; }

    public SqlServerContext(DbContextOptions<SqlServerContext> options) : base(options) { }

}
