using System.Text.Json.Serialization;

namespace TalentuInterview.Employees.Models
{
    public class Project
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }

        [JsonIgnore]
        public virtual ICollection<EmployeeProject>? EmployeeProject { get; }
    }
}
