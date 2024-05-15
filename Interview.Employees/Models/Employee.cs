using EmployeeBase = Interview.Shared.Models.Employee;

using System.Text.Json.Serialization;

namespace Interview.Employees.Models
{
    public class Employee: EmployeeBase
    {
        public virtual Role? Role { get; set; }

        [JsonIgnore]
        public virtual ICollection<EmployeeProject>? EmployeeProject { get; }
    }
}
