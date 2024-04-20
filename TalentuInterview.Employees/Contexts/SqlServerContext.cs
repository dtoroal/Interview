using Microsoft.EntityFrameworkCore;
using TalentuInterview.Employees.Models;

namespace TalentuInterview.Employees.Contexts;

public class SqlServerContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }

    public SqlServerContext(DbContextOptions<SqlServerContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        List<Role> newRoles = SetNewRoles(modelBuilder);
        List<Employee> newEmployees = SetNewEmployees(modelBuilder, newRoles);
        List<Project> newProjects = SetNewProjects(modelBuilder);
        SetNewEmployeeProjects(modelBuilder, newEmployees, newProjects);
    }

    private List<Employee> SetNewEmployees(ModelBuilder modelBuilder, List<Role> newRoles)
    {
        List<Employee> newEmployees = InitEmployees(newRoles);

        modelBuilder.Entity<Employee>(
            employee =>
            {
                employee.ToTable("Employee");
                employee.HasKey(e => e.Id);
                employee.Property(e => e.Id);
                employee.Property(e => e.Name).IsRequired().HasMaxLength(100);
                employee.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                employee.Property(e => e.Email).IsRequired().HasMaxLength(100);
                employee.Property(e => e.PhoneNumber).HasMaxLength(50);
                employee.Property(e => e.BirthdayDate).IsRequired().HasMaxLength(50);
                employee.HasOne(e => e.Role).WithMany(r => r.Employee).HasForeignKey(e => e.RoleId);

                employee.HasData(newEmployees);
            });

        return newEmployees;
    }

    private List<Role> SetNewRoles(ModelBuilder modelBuilder)
    {
        List<Role> newRoles = InitRoles();

        modelBuilder.Entity<Role>(
           employ =>
           {
               employ.ToTable("Role");
               employ.HasKey(r => r.Id);
               employ.Property(r => r.Id);
               employ.Property(r => r.Name).IsRequired().HasMaxLength(50);

               employ.HasData(newRoles);
           });

        return newRoles;
    }

    private List<Project> SetNewProjects(ModelBuilder modelBuilder)
    {
        List<Project> newProjects = InitProjects();

        modelBuilder.Entity<Project>(
            employ =>
            {
                employ.ToTable("Project");
                employ.HasKey(p => p.Id);
                employ.Property(p => p.Id);
                employ.Property(p => p.Name).IsRequired().HasMaxLength(100);

                employ.HasData(newProjects);
            });

        return newProjects;
    }

    private void SetNewEmployeeProjects(
        ModelBuilder modelBuilder,
        List<Employee> newEmployees,
        List<Project> newProjects
        )
    {
        List<EmployeeProject> newEmployeeProjects = InitEmployeeProjects(newEmployees, newProjects);

        modelBuilder.Entity<EmployeeProject>(
            project =>
            {
                project.ToTable("EmployeeProject");
                project.Property(ep => ep.Id).IsRequired();
                project.HasKey(ep => ep.Id);
                project.Property(ep => ep.EmployeeId).IsRequired();
                project.Property(ep => ep.ProjectId).IsRequired();
                project.HasOne(ep => ep.Project).WithMany(p => p.EmployeeProject).HasForeignKey(ep => ep.ProjectId);
                project.HasOne(ep => ep.Employee).WithMany(e => e.EmployeeProject).HasForeignKey(ep => ep.EmployeeId);

                project.HasData(newEmployeeProjects);

            });
    }

    private List<Employee> InitEmployees(List<Role> newRoles)
    {
        List<Employee> employeesInit =
        [
            new Employee()
            {
                Id = Guid.NewGuid(),
                Name = "Morty",
                LastName = "Smith",
                Email = "morty@randm.com",
                PhoneNumber = "1234567890",
                BirthdayDate = DateTime.Now.AddYears(-15).AddMonths(-7).AddDays(7),
                HashPassword = "gP33tSxUfbO0LU8v03M1frKYjZA4Bmt6BGU8H1EUQvk=",
                RoleId = newRoles[0].Id,
                HireDate = DateTime.Now.AddMonths(-1),
            },
            new Employee()
            {
                Id = Guid.NewGuid(),
                Name = "Rick",
                LastName = "Sánchez",
                Email = "rick@randm.com",
                PhoneNumber = "1234567890",
                BirthdayDate = DateTime.Now.AddYears(-39).AddMonths(-3).AddDays(5),
                HashPassword = "gP33tSxUfbO0LU8v03M1frKYjZA4Bmt6BGU8H1EUQvk=",
                RoleId = newRoles[0].Id,
                HireDate = DateTime.Now.AddMonths(-3),
            }
,
        ];
        return employeesInit;
    }

    private List<Role> InitRoles()
    {
        List<Role> initRoles =
        [
            new Role()
            {
                Id = Guid.NewGuid(),
                Name = "Administrator",
            }
,
            new Role()
            {
                Id = Guid.NewGuid(),
                Name = "User",
            }
,
        ];
        return initRoles;
    }

    private List<Project> InitProjects()
    {
        List<Project> initProjects =
        [
            new Project()
            {
                Id = Guid.NewGuid(),
                Name = "Licitación gobernación",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
            }
,
            new Project()
            {
                Id = Guid.NewGuid(),
                Name = "Administrador de recursos",
            }
,
        ];
        return initProjects;
    }

    private List<EmployeeProject> InitEmployeeProjects(List<Employee> newEmployees, List<Project> newProjects)
    {
        List<EmployeeProject> initEmployeeProjects =
        [
            new EmployeeProject()
            {
                Id = Guid.NewGuid(),
                EmployeeId = newEmployees[0].Id,
                ProjectId = newProjects[0].Id,
            },
            new EmployeeProject()
            {
                Id = Guid.NewGuid(),
                EmployeeId = newEmployees[0].Id,
                ProjectId = newProjects[1].Id,
            },
            new EmployeeProject()
            {
                Id = Guid.NewGuid(),
                EmployeeId = newEmployees[1].Id,
                ProjectId = newProjects[0].Id,
            }
,
        ];
        return initEmployeeProjects;
    }
}
