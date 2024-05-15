using Microsoft.EntityFrameworkCore;
using Interview.Authenticator.Models;

namespace Interview.Authenticator.Contexts;

public class SqlServerContext(DbContextOptions<SqlServerContext> options) : DbContext(options)
{
    public DbSet<Employee> Employee { get; set; }
}
