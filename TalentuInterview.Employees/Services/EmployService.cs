﻿using TalentuInterview.Employees.Contexts;
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

    public bool Update(EmployeeRequest employee)
    {
        try
        {
            Employee? employeeToUpdate = context.Employees.FirstOrDefault(e => e.Email == employee.Email);

            if (employeeToUpdate != null 
                && !string.IsNullOrEmpty(employee.Name)
                && !string.IsNullOrEmpty(employee.LastName))
            {
                employeeToUpdate.PhoneNumber = employee.PhoneNumber;
                employeeToUpdate.Name = employee.Name;
                employeeToUpdate.LastName = employee.LastName;

                context.Employees.Update(employeeToUpdate);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        } catch (Exception ex)
        {
            return false;
        }
    }

}

public interface IEmployeeService
{
    IEnumerable<Employee> Get();
    Employee? Get(string id);
    bool Update(EmployeeRequest employee);
}