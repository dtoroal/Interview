using Microsoft.EntityFrameworkCore;
using TalentuInterview.Authenticator.Models;

namespace TalentuInterview.Authenticator.Contexts;

public class SqlServerContext : DbContext
{
    public DbSet<User> User { get; set; }

    public SqlServerContext(DbContextOptions<SqlServerContext> options) : base(options) { }

}
