using System.Text.RegularExpressions;
using Interview.Employees.Contexts;
using Interview.Employees.Models;

namespace Interview.Employees.Services;

public class EmployService(SqlServerContext dbContext) : IEmployeeService
{
    private readonly SqlServerContext _context = dbContext;

    public IEnumerable<Employee> Get()
    {

        return _context.Employees;
    }

    public Employee? Get(string? email)
    {
        if (email != null)
        {
            return _context.Employees.FirstOrDefault(e => e.Email == email);
        }
        else
        {
            return null;
        }
    }

    public bool Update(EmployeeRequest employee)
    {
        try
        {
            if (!string.IsNullOrEmpty(employee.Name)
                && !string.IsNullOrEmpty(employee.LastName))
            {
                return UpdateEmployee(employee);
            }

            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool Delete(string? email)
    {
        try
        {
            Employee? employeeToUpdate = _context.Employees.FirstOrDefault(e => e.Email == email);

            if (employeeToUpdate != null)
            {
                _context.Employees.Remove(employeeToUpdate);
                _context.SaveChanges();
                return true;
            }
            else
            {
                throw new ArgumentException("Wrong email");
            }
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool Post(EmployeeRequest employee)
    {
        try
        {
            if (ValidateEmail(employee.Email) && ValidateLegalAge(employee))
            {
                Role? userRole = _context.Role.FirstOrDefault(r => r.Name == "User");

                if (userRole != null)
                {
                    Employee newEmployee = new()
                    {
                        Email = employee.Email,
                        Name = employee.Name,
                        LastName = employee.LastName,
                        BirthdayDate = employee.BirthdayDate,
                        PhoneNumber = employee.PhoneNumber,
                        HireDate = DateTime.UtcNow,
                        HashPassword = "gP33tSxUfbO0LU8v03M1frKYjZA4Bmt6BGU8H1EUQvk=",
                        RoleId = userRole.Id,
                        Image = employee.Image,
                    };

                    _context.Employees.Add(newEmployee);
                    _context.SaveChanges();

                    return true;
                }

                return false;
            }
            else
            {
                return false;
            }
        }
        catch (Exception)
        {
            return false;
        }
    }

    private bool ValidateLegalAge(EmployeeRequest employee)
    {
        if (employee.BirthdayDate >= DateTime.UtcNow.AddYears(-18))
        {
            return true;
        }
        else
        {
            throw new ArgumentException("The email is already registered");
        }
    }

    #region Private
    private bool UpdateEmployee(EmployeeRequest employee)
    {
        Employee? employeeToUpdate = _context.Employees.FirstOrDefault(e => e.Email == employee.Email);

        if (employeeToUpdate != null)
        {
            employeeToUpdate.PhoneNumber = employee.PhoneNumber;
            employeeToUpdate.Name = employee.Name;
            employeeToUpdate.LastName = employee.LastName;

            _context.Employees.Update(employeeToUpdate);
            _context.SaveChanges();
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool ValidateEmail(string email)
    {
        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        if (Regex.IsMatch(email, pattern))
        {
            Employee? employee = _context.Employees.FirstOrDefault(e => e.Email == email);

            if (employee == null)
            {
                return true;
            }
            else
            {
                throw new ArgumentException("Wrong email");
            }
        }
        else
        {
            throw new ArgumentException("The email is already registered");
        }
    }
    #endregion
}

public interface IEmployeeService
{
    /// <summary>
    /// Get every employee
    /// </summary>
    /// <returns>List of every employee</returns>
    IEnumerable<Employee> Get();

    /// <summary>
    /// Get an employee by email
    /// </summary>
    /// <param name="email">New user email</param>
    /// <returns>Employee found or null</returns>
    Employee? Get(string email);

    /// <summary>
    /// Update an employee
    /// </summary>
    /// <param name="employee">Employee data</param>
    /// <returns>True if the employee was updated</returns>
    bool Update(EmployeeRequest employee);

    /// <summary>
    /// Create a new employee
    /// </summary>
    /// <param name="employee">Employee data</param>
    /// <returns>True if the employee was created</returns>
    bool Post(EmployeeRequest employee);

    /// <summary>
    /// Delete an employee by email
    /// </summary>
    /// <param email="email">Employee data</param>
    /// <returns>True if the employee was created</returns>
    bool Delete(string? email);
}