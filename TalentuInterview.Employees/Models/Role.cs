using System.Text.Json.Serialization;

namespace TalentuInterview.Employees.Models
{
    public class Role
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Employee>? Employee { get; }
    }
}
