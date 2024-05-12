using System.Text.Json.Serialization;

namespace TalentuInterview.Employees.Models
{
    public class EmployeeProject
    {
        public required Guid Id { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid? ProjectId { get; set; }

        [JsonIgnore]
        public virtual Project? Project { get; }
        [JsonIgnore]
        public virtual Employee? Employee { get; }
    }
}
