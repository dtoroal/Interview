using System.Text.Json.Serialization;

namespace Interview.Employees.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime BirthdayDate { get; set; }
        public required string HashPassword { get; set; }
        public required DateTime HireDate { get; set; }
        public required Guid RoleId { get; set; }
        public virtual Role? Role { get; set; }

        [JsonIgnore]
        public virtual ICollection<EmployeeProject>? EmployeeProject { get; }
    }
}
