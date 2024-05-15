using System.ComponentModel.DataAnnotations.Schema;
using EmployeeBase = Interview.Shared.Models.Employee;

namespace Interview.Authenticator.Models;

public class Employee: EmployeeBase
{
    [NotMapped]
    public virtual string? Password { get; set; }
}
