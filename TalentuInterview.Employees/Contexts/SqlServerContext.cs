using Microsoft.EntityFrameworkCore;
using TalentuInterview.Employees.Models;

namespace TalentuInterview.Employees.Contexts;

public class SqlServerContext : DbContext
{
    public DbSet<Employ> Employs { get; set; }

    public SqlServerContext(DbContextOptions<SqlServerContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        List<Employ> newEmployees = InitEmployees();

        modelBuilder.Entity<Employ>(
            employ =>
            {
                employ.ToTable("Employ");
                employ.HasKey(e => e.Id);
                employ.Property(e => e.Id);
                employ.Property(e => e.Name).IsRequired().HasMaxLength(100);
                employ.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                employ.Property(e => e.Email).IsRequired().HasMaxLength(100);
                employ.Property(e => e.PhoneNumber).HasMaxLength(50);
                employ.Property(e => e.BirthdayDate).IsRequired().HasMaxLength(50);

                employ.HasData(newEmployees);
            });
    }

    private List<Employ> InitEmployees()
    {
        List<Employ> employeesInit = new List<Employ>();
        employeesInit.Add(
            new Employ()
            {
                Id = Guid.NewGuid(),
                Name = "Rick",
                LastName = "Sánchez",
                Email = "rick@randm.com",
                PhoneNumber = "1234567890",
                BirthdayDate = DateTime.Now,
            }
            );
        employeesInit.Add(
            new Employ()
            {
                Id = Guid.NewGuid(),
                Name = "Morty",
                LastName = "Smith",
                Email = "morty@randm.com",
                PhoneNumber = "1234567890",
                BirthdayDate = DateTime.Now,
            }
            );
        return employeesInit;
    }
}
